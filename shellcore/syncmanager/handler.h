//---------------------------------------------------------------------
//  This file is part of the Microsoft .NET Framework SDK Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//This source code is intended only as a supplement to Microsoft
//Development Tools and/or on-line documentation.  See these other
//materials for detailed information regarding Microsoft code samples.
// 
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//PARTICULAR PURPOSE.
//---------------------------------------------------------------------


#ifndef _HANDER_IMPL_
#define _HANDER_IMPL_

typedef struct _tagItemEvent
{
    SYNCMGRITEMID ItemID;
    HANDLE hEvent;
} ItemEvent;

class CWorkerThreadArgs;

class CSyncMgrHandler : public ISyncMgrSynchronize
{
private:
    LONG  m_cRef;
    CEnum *m_penumAllItems;
    CEnum *m_penumItemsToSync;
    DWORD m_dwSyncMgrFlags;
    ISyncMgrSynchronizeCallback *m_pSyncMgrSynchronizeCallback;
    DWORD m_cWorkerThreads;
    ItemEvent *m_rgitmevt;
    DWORD m_citmevt;
    HANDLE hCancelEvent;

public:
    CSyncMgrHandler();

    /* IUnknown */
    STDMETHODIMP            QueryInterface(REFIID, void**);
    STDMETHODIMP_(ULONG)    AddRef();
    STDMETHODIMP_(ULONG)    Release();

    /* ISyncMgrSynchronize methods */
    STDMETHODIMP    Initialize(DWORD dwReserved, DWORD dwSyncFlags,  DWORD cbCookie, const BYTE *lpCooke);
    STDMETHODIMP    GetHandlerInfo(LPSYNCMGRHANDLERINFO *ppSyncMgrHandlerInfo);
    STDMETHODIMP    EnumSyncMgrItems(ISyncMgrEnumItems **ppenumOffineItems);
    STDMETHODIMP    GetItemObject(REFSYNCMGRITEMID ItemID,REFIID riid,void** ppv);
    STDMETHODIMP    ShowProperties(HWND hWndParent,REFSYNCMGRITEMID ItemID);
    STDMETHODIMP    SetProgressCallback(ISyncMgrSynchronizeCallback *lpCallBack);
    STDMETHODIMP    PrepareForSync(ULONG cbNumItems, SYNCMGRITEMID* pItemIDs, HWND hWndParent, DWORD dwReserved);
    STDMETHODIMP    Synchronize(HWND hWndParent);
    STDMETHODIMP    SetItemStatus(REFSYNCMGRITEMID ItemID, DWORD dwSyncMgrStatus);
    STDMETHODIMP    ShowError(HWND hWndParent, REFSYNCMGRERRORID ErrorID);

private:
    ~CSyncMgrHandler();
    LPSYNCMGRSYNCHRONIZECALLBACK    _GetProgressCallback();
    STDMETHODIMP                    _CreateSyncItemWorkerThread(CWorkerThreadArgs *pwta);
    BOOL                            _IsLastThread();
    void                            _FindAndSetItemSkippedEvent(SYNCMGRITEMID ItemID);
    STDMETHODIMP                    _InitializeItems(BOOL fForce);


friend DWORD   WINAPI SyncItemWorkerThread(LPVOID lpArg);
};

// structure passed in CreateThread.
class CWorkerThreadArgs
{
public:
    CWorkerThreadArgs() : hSkip(NULL), hCancelAll(NULL), phndlr(NULL), pstrm(NULL), pitm(NULL), m_cRef(1)
    {
        DllAddRef();
    }
    ~CWorkerThreadArgs()
    {
        if (hSkip)
            CloseHandle(hSkip);

        // hCancel is owned by calling class so we don't have to free it.

        if (phndlr)
            phndlr->Release();

        if (pstrm)
            pstrm->Release();

        if (pitm)
            delete pitm;

        DllRelease();
    }
    void AddRef()
    {
        InterlockedIncrement(&m_cRef);
    }
    void Release()
    {
        if (!InterlockedDecrement(&m_cRef))
            delete this;
    }
    HRESULT Init(CSyncMgrHandler *phandler, HANDLE hGlobalCancelAll, LPSYNCMGRSYNCHRONIZECALLBACK pCallback)
    {
        HRESULT hr = E_OUTOFMEMORY;

        phndlr = phandler;
        phndlr->AddRef();
        pitm = new SYNCMGRITEM;
        if (pitm)
        {
            hr = CoMarshalInterThreadInterfaceInStream(IID_ISyncMgrSynchronizeCallback, pCallback, &pstrm);
            if (SUCCEEDED(hr))
            {
                // it's okay if these fail.
                hSkip = CreateEvent(NULL, FALSE, FALSE, NULL);
                hCancelAll = hGlobalCancelAll;
            }
        }

        return hr;
    }

public:
    HANDLE hSkip;            
    HANDLE hCancelAll;            
    CSyncMgrHandler *phndlr;    // pointer to base class
    IStream *pstrm;             // pointer to stream we'll use for marshalling the callback interface between threads.    
    SYNCMGRITEM *pitm;        // pointer to item that we're syncing

private:
    LONG m_cRef;
};

typedef struct _tagItemSettings
{
    SYNCMGRITEMID ItemID;
    WCHAR *pszName;
} ItemSettings;

// other methods
HRESULT ProgressHelper(ISyncMgrSynchronizeCallback *lpCallBack, REFSYNCMGRITEMID pItemID,
                UINT mask, WCHAR *pszStatusText, DWORD dwStatusType,
                int iProgValue, int iMaxValue);



#endif // #define _HANDER_IMPL_
