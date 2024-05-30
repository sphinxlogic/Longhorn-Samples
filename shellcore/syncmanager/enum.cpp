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


// this class will use comctl's HDPA for dynamic list management.
CEnum::CEnum()
{
    DllAddRef();
    m_cRef = 1;
    m_celt = 0;
    m_hdpa = NULL;
    m_wpos = 0;
}

int CALLBACK FreeSyncMgrItemCB(void *p, void *pData)
{
    LPSYNCMGRITEM pitm = (LPSYNCMGRITEM)p;
    delete pitm;
    return 1;
}

CEnum::~CEnum()
{
    DPA_DestroyCallback(m_hdpa, (PFNDPAENUMCALLBACK)FreeSyncMgrItemCB, NULL);
    DllRelease();
}

// IUnknown Members
STDMETHODIMP CEnum::QueryInterface(REFIID riid, void** ppv)
{
    *ppv = NULL;

    if (IsEqualIID(riid, IID_IUnknown) || IsEqualIID(riid, IID_ISyncMgrEnumItems))
    {
        *ppv = (ISyncMgrEnumItems*)this;
        AddRef();
        return S_OK;
    }
    return E_NOINTERFACE;
}

STDMETHODIMP_(ULONG) CEnum::AddRef()
{
    return InterlockedIncrement(&m_cRef);
}

STDMETHODIMP_(ULONG) CEnum::Release()
{
    ULONG cRef = InterlockedDecrement(&m_cRef);
    if ( 0 == cRef )
    {
        delete this;
    }
    return cRef;
}

// ISyncMgrEnumItems members
STDMETHODIMP CEnum::Next(ULONG celt, SYNCMGRITEM rgelt[], ULONG *pceltFetched)
{
    ULONG cItemsFetched;

    for (cItemsFetched = 0; (cItemsFetched < celt) && (m_wpos < m_celt); ++cItemsFetched, ++m_wpos)
    {
        LPSYNCMGRITEM pitm = (LPSYNCMGRITEM)DPA_GetPtr(m_hdpa, m_wpos);
        memcpy(&rgelt[cItemsFetched], pitm, sizeof(SYNCMGRITEM));
    }

    if (pceltFetched)
    {
        *pceltFetched = cItemsFetched;
    }
    return (cItemsFetched == celt ? S_OK : S_FALSE);
}

STDMETHODIMP CEnum::Clone(ISyncMgrEnumItems **ppenum)
{
    *ppenum = NULL;
    return E_NOTIMPL;
}

STDMETHODIMP CEnum::Skip(ULONG celt)
{
    return E_NOTIMPL;
}

STDMETHODIMP CEnum::Reset()
{
    m_wpos = 0;
    return S_OK;
}

// public methods
// Add takes ownership of pelt
STDMETHODIMP CEnum::Add(LPSYNCMGRITEM pelt)
{
    HRESULT hr = S_OK;
    if (!m_hdpa)
    {
        hr = _Init();
    }

    if (SUCCEEDED(hr))
    {
        if (DPA_InsertPtr(m_hdpa, m_celt, pelt) != -1)
        {
            ++m_celt;
            pelt = NULL;
        }  
    }
    
    // delete takes NULL and does nothing.
    delete pelt;

    return S_OK;
}

STDMETHODIMP CEnum::FindByID(SYNCMGRITEMID ItemID, LPSYNCMGRITEM *ppelt)
{
    HRESULT hr = E_FAIL;

    *ppelt = NULL;

    // we just search our enum for this Item
    for (DWORD dw = 0; dw < m_celt; ++dw)
    {
        SYNCMGRITEM *peltWorking = (SYNCMGRITEM*)DPA_GetPtr(m_hdpa, dw);
        
        if (peltWorking && IsEqualGUID(ItemID, peltWorking->ItemID))
        {
            *ppelt = new SYNCMGRITEM;
            if (*ppelt)
            {
                memcpy(*ppelt, peltWorking, sizeof(SYNCMGRITEM));
                hr = S_OK;
            }
            else
            {
                hr = E_OUTOFMEMORY;
            }
            break;
        }
    }

    return hr;
}

DWORD CEnum::get_Count()
{
    return m_celt;
}

// private methods
STDMETHODIMP CEnum::_Init()
{
    m_hdpa = DPA_Create(1);
    return (m_hdpa ? S_OK : E_OUTOFMEMORY);
}

