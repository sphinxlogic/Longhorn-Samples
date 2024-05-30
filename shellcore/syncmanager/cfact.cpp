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


//+---------------------------------------------------------------------------
//
//  Member:     CClassFactory::CClassFactory, public
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

CClassFactory::CClassFactory() : m_cRef(1)
{
    DllAddRef();
}

//+---------------------------------------------------------------------------
//
//  Member:     CClassFactory::~CClassFactory, public
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

CClassFactory::~CClassFactory()
{
    DllRelease();
}

//+---------------------------------------------------------------------------
//
//  Member:     CClassFactory::QueryInterface public
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

STDMETHODIMP CClassFactory::QueryInterface(REFIID riid, void **ppv)
{
    *ppv = NULL;

    // Any interface on this object is the object pointer
    if (IsEqualIID(riid, IID_IUnknown) || IsEqualIID(riid, IID_IClassFactory))
    {
        *ppv = (IClassFactory*)this;
        AddRef();
        return S_OK;
    }

    return E_NOINTERFACE;
}

//+---------------------------------------------------------------------------
//
//  Member:     CClassFactory::AddRef, public
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

STDMETHODIMP_(ULONG) CClassFactory::AddRef()
{
    return ++m_cRef;
}


//+---------------------------------------------------------------------------
//
//  Member:     CClassFactory::Release, public
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

STDMETHODIMP_(ULONG) CClassFactory::Release()
{
    if (--m_cRef)
        return m_cRef;

    delete this;
    return 0L;
}


//+---------------------------------------------------------------------------
//
//  Member:     CClassFactory::CreateInstance, public
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

STDMETHODIMP CClassFactory::CreateInstance(LPUNKNOWN pUnkOuter, REFIID riid, void **ppv)
{
    *ppv = NULL;

    if (pUnkOuter)
        return CLASS_E_NOAGGREGATION;

    ISyncMgrSynchronize *pSyncMgrHandler = new CSyncMgrHandler();

    if (NULL == pSyncMgrHandler)
        return E_OUTOFMEMORY;

    HRESULT hr =  pSyncMgrHandler->QueryInterface(riid, ppv);
    pSyncMgrHandler->Release();
    return hr;
}


//+---------------------------------------------------------------------------
//
//  Member:     CClassFactory::LockServer, public
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

STDMETHODIMP CClassFactory::LockServer(BOOL fLock)
{
    if (fLock)
    {
       DllAddRef();
    }
    else
    {
       DllRelease();
    }
    return S_OK;
}
