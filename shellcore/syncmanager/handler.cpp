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


#include "precomp.h"

#define MAX_PROG_VALUE 100

// worker thread entry point
DWORD WINAPI SyncItemWorkerThread(void *pv);

HRESULT CExtractIcon_Create(REFGUID ItemID, REFIID riid, void **ppv);
HRESULT CExtractImage_Create(REFGUID ItemID, REFIID riid, void **ppv);


//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::CSyncMgrHandler, public
//
//  Synopsis:   Constructor
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------


CSyncMgrHandler::CSyncMgrHandler()
{
    DllAddRef();
    m_cRef = 1;
    m_penumAllItems = NULL;
    m_penumItemsToSync = NULL;
    m_dwSyncMgrFlags = 0;
    m_pSyncMgrSynchronizeCallback = NULL;
    m_cWorkerThreads = 0;
    m_rgitmevt = NULL;
    hCancelEvent = NULL;
    m_citmevt = 0;
}


//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::~CSyncMgrHandler, public
//
//  Synopsis:   Destructor
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

CSyncMgrHandler::~CSyncMgrHandler()
{
    if (m_penumAllItems)
        m_penumAllItems->Release();

    if (m_pSyncMgrSynchronizeCallback)
        m_pSyncMgrSynchronizeCallback->Release();

    if (m_rgitmevt)
        delete [] m_rgitmevt;

    if (hCancelEvent)
        CloseHandle(hCancelEvent);

    DllRelease();
}


//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::QueryInteface, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP CSyncMgrHandler::QueryInterface(REFIID riid, void **ppv)
{
    HRESULT hr = E_NOINTERFACE;

    *ppv = NULL;

    if (IsEqualIID(riid, IID_IUnknown) || IsEqualIID(riid, IID_ISyncMgrSynchronize))
    {
        *ppv = (ISyncMgrSynchronize*)this;
        AddRef();
        hr = S_OK;
    }

    return hr;
}

//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::AddRef, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP_(ULONG) CSyncMgrHandler::AddRef()
{
    return InterlockedIncrement(&m_cRef);
}

//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::Release, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP_(ULONG) CSyncMgrHandler::Release()
{
    ULONG cRef = InterlockedDecrement(&m_cRef);
    if ( 0 == cRef )
    {
        delete this;
    }
    return cRef;
}


//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::Initialize, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP CSyncMgrHandler::Initialize(DWORD dwReserved, DWORD dwSyncFlags, DWORD cbCookie, const BYTE *lpCooke)
{
    // SyncMgr will call this immeidately after creating an instance of our handler
    // it could be instantiating us for many reasons:
    //      -> GetHandlerInfo() call
    //      -> PrepareForSync() and Synchronize() call
    //      -> EnumSyncMgrItems() call

    // Exactly one of the following values will be passed in.
    // Bitwise & SYNCMGRFLAG_EVENTMASK with dwSyncFlags to discern which one.
    //  SYNCMGRFLAG_CONNECT	= 0x1,
	//  SYNCMGRFLAG_PENDINGDISCONNECT	= 0x2,
	//  SYNCMGRFLAG_MANUAL	= 0x3,
	//  SYNCMGRFLAG_IDLE	= 0x4,
	//  SYNCMGRFLAG_INVOKE	= 0x5,
	//  SYNCMGRFLAG_SCHEDULED	= 0x6,
    // 
	//  SYNCMGRFLAG_EVENTMASK	= 0xff,

    // One or more of the following values may be present as well.
	//  SYNCMGRFLAG_SETTINGS	= 0x100,
	//  SYNCMGRFLAG_MAYBOTHERUSER	= 0x200

    // If SYNCMGRFLAG_INVOKE is passed in, the cookie data (lpCookie) will be the same as 
    // what was passed to ISyncMgrInvoke::UpdateItems().  The cookie data is
    // NULL otherwise.

    // save flags for later reference
    m_dwSyncMgrFlags = dwSyncFlags;

    // returning anything other than S_OK will cause us to be unloaded
    // without enumerating items or synchronizing.
    return S_OK;
}





//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::GetHandlerInfo, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP CSyncMgrHandler::GetHandlerInfo(LPSYNCMGRHANDLERINFO *ppSyncMgrHandlerInfo)
{
    HRESULT hr;

    LPSYNCMGRHANDLERINFO pSyncInfo = (LPSYNCMGRHANDLERINFO)CoTaskMemAlloc(sizeof(*pSyncInfo));
    if (pSyncInfo)
    {
        pSyncInfo->cbSize = sizeof(SYNCMGRHANDLERINFO);
        //  For Longhorn and above implement IExtractIcon or IExtractImage to enable high-resolution icons.
        //  Return the HICON here only if you are concerned with downlevel compatability.
        pSyncInfo->hIcon = LoadIcon(g_hmodThisDll, MAKEINTRESOURCE(IDI_SAMPLEHANDLERICON));
        pSyncInfo->SyncMgrHandlerFlags = SYNCMGRHANDLER_HASPROPERTIES;
        pSyncInfo->SyncMgrHandlerFlags |= SYNCMGRHANDLER_ALWAYSLISTHANDLER;
        StringCchCopy(pSyncInfo->wszHandlerName, ARRAYSIZE(pSyncInfo->wszHandlerName), L"Sample Handler Name");
        hr = S_OK;
    }
    else
        hr = E_OUTOFMEMORY;

    *ppSyncMgrHandlerInfo = pSyncInfo;

    return hr;
}


//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::GetItemObject, public
//
//  Synopsis:   This is new functionality for Longhorn.  Now this is called in
//              order to obtain a high-quality icon for display in the new
//              SyncMgr CPL Shell Folder.
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP CSyncMgrHandler::GetItemObject(REFSYNCMGRITEMID ItemID, REFIID riid, void** ppv)
{
    HRESULT hr = E_NOTIMPL;

    *ppv = NULL;

    // Note that there is no reason to implement both IExtractIcon
    // and IExtractImage. One is enough. We are only showing both
    // for demonstration purposes.
    if (IsEqualIID(riid, IID_IExtractIconW))
    {
        hr = CExtractIcon_Create(ItemID, riid, ppv);
    }
    else if (IsEqualIID(riid, IID_IExtractImage))
    {
        hr = CExtractImage_Create(ItemID, riid, ppv);
    }

    return hr;
}

//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::EnumSyncMgrItems, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP CSyncMgrHandler::EnumSyncMgrItems(ISyncMgrEnumItems** ppSyncMgrEnumItems)
{
    HRESULT hr = _InitializeItems(TRUE);
    if (SUCCEEDED(hr))
    {
        hr = m_penumAllItems->QueryInterface(IID_ISyncMgrEnumItems, (void**)ppSyncMgrEnumItems);
    }
    return hr;
}

//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::ShowProperties, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP CSyncMgrHandler::ShowProperties(HWND hWndParent, REFSYNCMGRITEMID ItemID)
{
    ISyncMgrSynchronizeCallback *pCallback = _GetProgressCallback();

    // Show a properties/configuration dialog here.
    //
    // Options:
    // 1. Modal UI on this thread.  Block here and call ShowPropertiesCompleted below.
    // 2. Modeless UI.  Do this on another thread (with a message pump) and call
    //    ShowPropertiesCompleted below.
    // 3. Modal UI on another thread.  Marshal pCallback to the other thread and
    //    call ShowPropertiesCompleted from that thread when the UI is dismissed.
    //
    // Note that there is no need to call pCallback->EnableModeless here, since
    // ShowProperties is expected to display UI of some sort. However, it is important
    // to call ShowPropertiesCompleted at some point so Sync Manager can reenable
    // its own UI.

    MessageBox(hWndParent, L"Show Properties Here", L"Sample Sync Handler", MB_OK | MB_ICONINFORMATION | MB_TOPMOST);

    if (pCallback)
    {
        // Call ShowPropertiesCompleted here for options 1 and 2 above.
        //
        // Pass one of the following two values to ShowPropertiesCompleted() if your Sync content (Items)
        // has changed as a result of the ShowProperties() call.  All other values are ignored.
        //  S_SYNCMGR_ITEMDELETED     
        //  S_SYNCMGR_ENUMITEMS         
        pCallback->ShowPropertiesCompleted(S_OK);
        pCallback->Release();
    }
    return S_OK;
}




//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::SetProgressCallback, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP CSyncMgrHandler::SetProgressCallback(ISyncMgrSynchronizeCallback *pCallBack)
{
    if (m_pSyncMgrSynchronizeCallback)
        m_pSyncMgrSynchronizeCallback->Release();

    m_pSyncMgrSynchronizeCallback = pCallBack;

    if (m_pSyncMgrSynchronizeCallback)
        m_pSyncMgrSynchronizeCallback->AddRef();

    return NOERROR;
}


//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::PrepareForSync, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP CSyncMgrHandler::PrepareForSync(ULONG cbNumItems, SYNCMGRITEMID *pItemIDs, HWND hWndParent, DWORD dwReserved)

{
    // here is where we'd do our work if we had any to do.
    // we've already read our items in in Initialize() and for the
    // sake of this sample, that's all the work we need to do.

    // we don't have any UI to show, but if we did, we would check to see if the
    // MAYBOTHERUSER flag had been set
    if (m_dwSyncMgrFlags & SYNCMGRFLAG_MAYBOTHERUSER)
    {
        // show UI if we need to
    }

    LPSYNCMGRSYNCHRONIZECALLBACK pCallback = _GetProgressCallback();

    // we are given an array of ItemIDs (GUIDs)
    // so we will just find the correct SyncMgrItems and copy them into our ItemsToSync enum

    HRESULT hr = _InitializeItems(FALSE);

    if (SUCCEEDED(hr))
    {
        m_penumItemsToSync = new CEnum();
        if (m_penumItemsToSync)
        {
            hr = S_OK;
            for (DWORD dwCurItemIndex = 0;dwCurItemIndex < cbNumItems;++dwCurItemIndex)
            {
                LPSYNCMGRITEM pitm = NULL;
                
                if (SUCCEEDED(m_penumAllItems->FindByID(pItemIDs[dwCurItemIndex], &pitm)))
                {
                    // Add takes ownership of pitm
                    m_penumItemsToSync->Add(pitm);
                }
                else
                {
                    // we set our progress to FAILED if we couldn't find it.
                    // also set progress max 100 and cur val 100.
                    ProgressHelper(pCallback, 
                            pItemIDs[dwCurItemIndex],
                            SYNCMGRPROGRESSITEM_PROGVALUE | SYNCMGRPROGRESSITEM_MAXVALUE | SYNCMGRPROGRESSITEM_STATUSTYPE,
                            NULL,
                            SYNCMGRSTATUS_FAILED,
                            100,
                            100);
                }
            }
        }
        else
        {
            hr = E_OUTOFMEMORY;
        }
    }

    if (pCallback)
    {
        pCallback->PrepareForSyncCompleted(hr);   // returning anything other than S_OK will result in a halting of the Sync and our handler being freed.
        pCallback->Release();
    }

    return hr;
}


//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::Synchronize, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP CSyncMgrHandler::Synchronize(HWND hWndParent)
{
    // This call happens on the main Sync Mgr UI thread so we don't want to do any expensive work
    // here.  All synchronization work should happen on another thread so that this call can return
    // as soon as possible. How many threads you create depends on the design of your handler and how 
    // many items it has.
    //
    // Keep in mind that a good definition for Sync Item is: a container of related things.  For
    // example, email contacts might be one Sync Item whereas each individual contact would not be.

    if (!m_penumItemsToSync)
        return E_UNEXPECTED;

    LPSYNCMGRSYNCHRONIZECALLBACK pCallback = _GetProgressCallback();
    if (pCallback)
    {
        if (m_penumItemsToSync->get_Count() == 0)
        {
            // there is no work to be done.
            pCallback->SynchronizeCompleted(S_OK);
        }
        else
        {
            m_penumItemsToSync->Reset();

            // this is our global cancelled event.  we'll pass a copy of this handle to each thread.
            HANDLE hCancelAll = CreateEvent(NULL, FALSE, FALSE, NULL);

            m_rgitmevt = new ItemEvent[m_penumItemsToSync->get_Count()];

            for (DWORD dw = 0; dw < m_penumItemsToSync->get_Count(); ++dw)
            {
                // here we will create our threads, 1 for each Item
                // we will pass in 
                //      - a pointer to our base handler class (this)
                //      - a pointer to a stream that contains our InterThread marshalled ProgressCallback interface
                //      - the Item that this particular thread cares about
                //
                
                CWorkerThreadArgs *pwta = new CWorkerThreadArgs();
                if (pwta)
                {
                    if (SUCCEEDED(pwta->Init(this, hCancelAll, pCallback)))
                    {
                        if (S_OK == m_penumItemsToSync->Next(1, pwta->pitm, NULL))
                        {
                            ItemEvent itmevt;
                            itmevt.ItemID = pwta->pitm->ItemID;
                            itmevt.hEvent = pwta->hSkip;
                            m_rgitmevt[dw] = itmevt;
                            ++m_citmevt;

                            // now we spin off and create the thread
                            _CreateSyncItemWorkerThread(pwta);
                        }

                    }
                    pwta->Release();
                }
            }
        }
        pCallback->Release();
    }

    if (m_penumItemsToSync)
    {
        m_penumItemsToSync->Release();
        m_penumItemsToSync = NULL;
    }

    return NOERROR;
}





//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::SetItemStatus, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP CSyncMgrHandler::SetItemStatus(REFSYNCMGRITEMID ItemID, DWORD dwSyncMgrStatus)
{
    // Valid parameter combinations are:
    //   GUID_NULL + SYNCMGRSTATUS_STOPPED (cancel all)
    //   ItemID + SYNCMGRSTATUS_SKIPPED (skip this item)
    //   ItemID + SYNCMGRSTATUS_DELETED (only if we set SYNCMGRITEM_MAYDELETEITEM)

    if ((dwSyncMgrStatus & SYNCMGRSTATUS_SKIPPED) ||
        ((dwSyncMgrStatus & SYNCMGRSTATUS_STOPPED) && IsEqualGUID(ItemID, GUID_NULL)))
    {
        _FindAndSetItemSkippedEvent(ItemID);
    }

    return NOERROR;
}

//+---------------------------------------------------------------------------
//
//  Member:     CSyncMgrHandler::ShowError, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDMETHODIMP CSyncMgrHandler::ShowError(HWND hWndParent,REFSYNCMGRERRORID ErrorID)
{

    // Here is where we'd use our UI to show the detailed error info corresponding
    // to the ErrorID we logged in ISyncMgrSynchronizeCallback::LogError().
    return E_NOTIMPL;
}


// private methods
LPSYNCMGRSYNCHRONIZECALLBACK CSyncMgrHandler::_GetProgressCallback()
{
    if (m_pSyncMgrSynchronizeCallback)
        m_pSyncMgrSynchronizeCallback->AddRef();

    return m_pSyncMgrSynchronizeCallback;
}

STDMETHODIMP CSyncMgrHandler::_CreateSyncItemWorkerThread(CWorkerThreadArgs *pwta)
{
    pwta->AddRef();
    HANDLE hRawThread = CreateThread(NULL, 0, SyncItemWorkerThread, pwta, 0, NULL);

    HRESULT hr;
    if (hRawThread)
    {
        CloseHandle(hRawThread);
        hr = S_OK;
        ++m_cWorkerThreads;
    }
    else
    {
        hr = E_FAIL;
        pwta->Release();
    }

    return hr;
}

BOOL CSyncMgrHandler::_IsLastThread()
{
    return (InterlockedDecrement((LONG*)&m_cWorkerThreads) == 0 ? TRUE : FALSE);
}

void CSyncMgrHandler::_FindAndSetItemSkippedEvent(SYNCMGRITEMID ItemID)
{
    if (m_rgitmevt)
    {
        for (DWORD dw = 0; dw < m_citmevt; ++dw)
        {
            if (IsEqualGUID(ItemID, m_rgitmevt[dw].ItemID) || IsEqualGUID(ItemID, GUID_NULL))
            {
                if (m_rgitmevt[dw].hEvent)
                    SetEvent(m_rgitmevt[dw].hEvent);
            }
        }
    }
}

HRESULT CSyncMgrHandler::_InitializeItems(BOOL fForce)
{
    HRESULT hr = S_OK;
    if (!m_penumAllItems || fForce)
    {
        if (m_penumAllItems)
            m_penumAllItems->Release();

        m_penumAllItems = new CEnum();
        if (m_penumAllItems)
        {
            // this helper (reg.cpp) will create the reg key if it can't open it.
            HKEY hkeyHandler = NULL;
            if ((hkeyHandler = CreateHandlerPrefKey(CLSID_SyncMgrHandler)))
            {
                WCHAR szName[MAX_PATH] = {0};
                CLSID clsid;
                for (DWORD dwIndex = 0; RegEnumKey(hkeyHandler, dwIndex, szName, ARRAYSIZE(szName)) == ERROR_SUCCESS; ++dwIndex)
                {
                    // ItemID's should be persisted and reused, not created every time an item is asked for, as Sync Manager
                    // tracks user preferences for items using the ItemID.
                    if (SUCCEEDED(CLSIDFromString(szName, &clsid)))
                    {
                        LPSYNCMGRITEM pitm = new SYNCMGRITEM;
                        if (pitm)
                        {
                            pitm->cbSize        = sizeof(SYNCMGRITEM);
                            pitm->dwItemState   = SYNCMGRITEMSTATE_CHECKED;
                            //  For Longhorn and above implement IExtractIcon or IExtractImage to enable high-resolution icons.
                            //  Return the HICON here only if you are concerned with downlevel compatability.
                            pitm->hIcon         = LoadIcon(g_hmodThisDll,MAKEINTRESOURCE(IDI_SAMPLEHANDLERICON));   
                            pitm->dwFlags       = SYNCMGRITEM_HASPROPERTIES;
                            pitm->ItemID        = clsid;
                            StringCchCopy(pitm->wszItemName, ARRAYSIZE(pitm->wszItemName), L"Sample Item Name"); // we do store this info in the registry, I just ran out of time with this sample.

                            // Add takes ownership
                            m_penumAllItems->Add(pitm);
                        }
                        else
                        {
                            hr = E_OUTOFMEMORY;
                            m_penumAllItems->Release();
                            m_penumAllItems = NULL;
                            break;
                        }
                    } 
                }
                RegCloseKey(hkeyHandler);
            }
            else
            {
                hr = E_FAIL;
            }
        }
        else
        {
            hr = E_OUTOFMEMORY;
        }
    }
    return hr;
}


// non CSyncMgrHandler methods
HRESULT ProgressHelper(ISyncMgrSynchronizeCallback *lpCallBack, REFSYNCMGRITEMID pItemID,
                                UINT mask, WCHAR *pszStatusText, DWORD dwStatusType,
                                int iProgValue, int iMaxValue)
{
    SYNCMGRPROGRESSITEM syncProg;
    syncProg.cbSize = sizeof(SYNCMGRPROGRESSITEM);
    syncProg.mask = mask;

    if (SYNCMGRPROGRESSITEM_STATUSTEXT & mask)
    {
        syncProg.lpcStatusText = pszStatusText;
    }

    syncProg.dwStatusType = dwStatusType;
    syncProg.iProgValue = iProgValue;
    syncProg.iMaxValue = iMaxValue;

    HRESULT hr = S_OK;
    if (lpCallBack)
        hr = lpCallBack->Progress(pItemID, &syncProg);

    return hr;
}

DWORD WINAPI SyncItemWorkerThread(void *pv)
{
    CWorkerThreadArgs *pwta = (CWorkerThreadArgs*)pv;
    if (pwta)
    {
        // we must unmarshall the SyncMgrCallback interface
        ISyncMgrSynchronizeCallback *pCallback = NULL;

        HRESULT hr = CoGetInterfaceAndReleaseStream(pwta->pstrm, IID_ISyncMgrSynchronizeCallback, (void**)&pCallback);
        pwta->pstrm = NULL;

        if (SUCCEEDED(hr))
        {
            // first we set the maxvalue
            ProgressHelper(pCallback,
                    pwta->pitm->ItemID,
                    SYNCMGRPROGRESSITEM_MAXVALUE | SYNCMGRPROGRESSITEM_STATUSTYPE,
                    NULL,
                    SYNCMGRSTATUS_UPDATING,
                    0,
                    MAX_PROG_VALUE);

            // here we do the actual "sync"
            // we must always check as frequently as possible to make sure that the user hasn't cancelled or skipped
            BOOL fCancelled = FALSE;
            BOOL fSkipped = FALSE;

            for (DWORD dw = 0; (dw < MAX_PROG_VALUE) && !fSkipped && !fCancelled; ++dw)
            {
                // this sample does no actual work.  we sleep every time through the loop
                // so we don't finish immediately.
                Sleep(100);

                WCHAR szStatusText[MAX_PATH];
                StringCchPrintf(szStatusText, ARRAYSIZE(szStatusText), L"Item %d of %d", dw, MAX_PROG_VALUE);
                HRESULT hr = ProgressHelper(pCallback,
                                        pwta->pitm->ItemID,
                                        SYNCMGRPROGRESSITEM_PROGVALUE | SYNCMGRPROGRESSITEM_STATUSTEXT,
                                        szStatusText,
                                        (dw == MAX_PROG_VALUE ? SYNCMGRSTATUS_SUCCEEDED : SYNCMGRSTATUS_UPDATING),
                                        dw, // this is the new progress value
                                        0);

                if (hr == S_SYNCMGR_CANCELITEM)
                {
                    fSkipped = TRUE;
                }
                else if (hr == S_SYNCMGR_CANCELALL)
                {
                    fCancelled = TRUE;
                    SetEvent(pwta->hCancelAll);
                }

                if (!fCancelled)
                {
                    // we must check to see if the user has asked us to skip or cancel
                    if (WaitForSingleObject(pwta->hSkip, 0) == WAIT_OBJECT_0)
                        fSkipped = TRUE;

                    if (WaitForSingleObject(pwta->hCancelAll, 0) == WAIT_OBJECT_0)
                        fCancelled = TRUE;
                }
            }

            // here we set our final state.
            if (fSkipped)
            {
                ProgressHelper(pCallback,
                    pwta->pitm->ItemID,
                    SYNCMGRPROGRESSITEM_STATUSTYPE,
                    NULL,
                    SYNCMGRSTATUS_SKIPPED,
                    0,
                    0);
            }
            else if (fCancelled)
            {
                ProgressHelper(pCallback,
                    pwta->pitm->ItemID,
                    SYNCMGRPROGRESSITEM_STATUSTYPE,
                    NULL,
                    SYNCMGRSTATUS_STOPPED,
                    0,
                    0);
            }
            else 
            {
                ProgressHelper(pCallback,
                    pwta->pitm->ItemID,
                    SYNCMGRPROGRESSITEM_STATUSTYPE,
                    NULL,
                    SYNCMGRSTATUS_SUCCEEDED,
                    0,
                    0);
            }

            // at this point, we are finished sync'ing
            // we need to check to see if we were the last Item to finish 
            if (pwta->phndlr->_IsLastThread())
            {
                // we must call SynchronizeCompleted() when the last thread has finished.
                pCallback->SynchronizeCompleted(S_OK);
            }

            pCallback->Release();
        }
        // if we failed to get the callback, something heinous is wrong and we should bail gracefully.
        pwta->Release();
    }
    return 0;
}


class CExtractIcon : public IExtractIconW
{
public:
    // IUnknown
    STDMETHODIMP QueryInterface(REFIID riid, void **ppv)
    {
        HRESULT hr = E_NOINTERFACE;
        *ppv = NULL;
        if (riid == IID_IUnknown || riid == IID_IExtractIconW)
        {
            *ppv = (IExtractIconW*)this;
            AddRef();
            hr = S_OK;
        }
        return hr;
    }
    STDMETHODIMP_(ULONG) AddRef()
    {
        return InterlockedIncrement(&_cRef);
    }
    STDMETHODIMP_(ULONG) Release()
    {
        ULONG cRef = InterlockedDecrement(&_cRef);
        if ( 0 == cRef )
        {
            delete this;
        }
        return cRef;
    }

    // IExtractIconW
    STDMETHODIMP GetIconLocation(UINT uFlags, LPWSTR pszIconFile, UINT cchMax, int* piIndex, UINT* pwFlags)
    {
        // All of our icons live in this DLL.
        GetModuleFileNameW(g_hmodThisDll, pszIconFile, cchMax);

        *pwFlags = 0;   // unused

        if (IsEqualGUID(GUID_NULL, _guid))
        {                                 
            // We are being asked for our handler icon (GUID_NULL)         
            *piIndex = -IDI_HANDLERLEVELICON;
        }
        else
        {
            // We are being asked for an item icon.
            // We should check the GUID is actually one of ours, but
            // let's just return the same icon for all items.
            *piIndex = -IDI_HANDLERLEVELICON;
        }
        return S_OK;
    }
    STDMETHODIMP Extract(LPCWSTR pszFile, UINT nIconIndex, HICON* phiconLarge, HICON* phiconSmall, UINT nIconSize)
    {
        // Implement this method if your icons are not stored as resources.
        // We supplied a resource ID above. The caller can extract from there.
        return S_FALSE;
    }

private:
    CExtractIcon(REFGUID rguid) : _cRef(1), _guid(rguid)
    {
        DllAddRef();
    }

    ~CExtractIcon()
    {
        DllRelease();
    }

    LONG _cRef;
    GUID _guid;

    friend HRESULT CExtractIcon_Create(REFGUID ItemID, REFIID riid, void **ppv);
};

HRESULT CExtractIcon_Create(REFGUID ItemID, REFIID riid, void **ppv)
{
    HRESULT hr;
    CExtractIcon *pxi = new CExtractIcon(ItemID);
    if (pxi)
    {
        hr = pxi->QueryInterface(riid, ppv);
        pxi->Release();
    }
    else
    {
        hr = E_OUTOFMEMORY;
    }
    return hr;
}


class CExtractImage : public IExtractImage
{
public:
    // IUnknown
    STDMETHODIMP QueryInterface(REFIID riid, void **ppv)
    {
        HRESULT hr = E_NOINTERFACE;
        *ppv = NULL;
        if (riid == IID_IUnknown || riid == IID_IExtractImage)
        {
            *ppv = (IExtractImage*)this;
            AddRef();
            hr = S_OK;
        }
        return hr;
    }
    STDMETHODIMP_(ULONG) AddRef()
    {
        return InterlockedIncrement(&_cRef);
    }
    STDMETHODIMP_(ULONG) Release()
    {
        ULONG cRef = InterlockedDecrement(&_cRef);
        if ( 0 == cRef )
        {
            delete this;
        }
        return cRef;
    }

    // IExtractImage
    STDMETHODIMP GetLocation(LPWSTR pszPathBuffer, DWORD cchMax, DWORD *pdwPriority, const SIZE *prgSize, DWORD dwRecClrDepth, DWORD *pdwFlags)
    {
        HRESULT hr = E_FAIL;

        if (IsEqualGUID(GUID_NULL, _guid))  // this is handler level icon request
        {
            *pdwPriority = 0;

            // for now we won't care about size requested or color depth
            *pdwFlags |= IEIFLAG_CACHE; // we want the shell to cache our image, we won't write that logic.
            
            // instead of a path, we'll pass back a string GUID_NULL to keep our internal logic consistent.
            if (!StringFromGUID2(_guid, pszPathBuffer, cchMax))
                hr = E_OUTOFMEMORY;
            else
                hr = NOERROR;
        }

        return hr;
    }
    STDMETHODIMP Extract(HBITMAP *phBmpImage)
    {

        HRESULT hr = E_FAIL;
        *phBmpImage = NULL;
        if (IsEqualGUID(GUID_NULL, _guid))
        {
            // we set LR_SHARED so that the system will take care of destroying this when it's no longer needed.
            *phBmpImage = (HBITMAP)LoadImage(g_hmodThisDll, MAKEINTRESOURCE(IDI_HANDLERLEVELICONBMP), IMAGE_BITMAP, 0, 0, LR_SHARED);
            if (*phBmpImage)
            {
                hr = NOERROR;
            }
        }
        else
        {
            // we won't return any hi-res icons for anything other than the handler
            // to force exercise of the IExtractIcon interface.
        }

        return hr;
    }

private:
    CExtractImage(REFGUID rguid) : _cRef(1), _guid(rguid)
    {
        DllRelease();
    }

    ~CExtractImage()
    {
        DllRelease();
    }

    LONG _cRef;
    GUID _guid;

    friend HRESULT CExtractImage_Create(REFGUID ItemID, REFIID riid, void **ppv);
};


HRESULT CExtractImage_Create(REFGUID ItemID, REFIID riid, void **ppv)
{
    HRESULT hr;
    CExtractImage *pxi = new CExtractImage(ItemID);
    if (pxi)
    {
        hr = pxi->QueryInterface(riid, ppv);
        pxi->Release();
    }
    else
    {
        hr = E_OUTOFMEMORY;
    }
    return hr;
}