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


#ifndef _ENUMERATOR_CLASS_
#define _ENUMERATOR_CLASS_


class CEnum : public ISyncMgrEnumItems
{
private:
    LONG m_cRef;
    HDPA  m_hdpa;
    DWORD m_celt;
    DWORD m_wpos;

public:
    CEnum();
    ~CEnum();

    // IUnknown Members
    STDMETHODIMP            QueryInterface(REFIID, void **);
    STDMETHODIMP_(ULONG)    AddRef();
    STDMETHODIMP_(ULONG)    Release();

    // ISyncMgrEnumItems members
    STDMETHODIMP Next(ULONG celt, LPSYNCMGRITEM rgelt, ULONG *pceltFetched);
    STDMETHODIMP Clone(ISyncMgrEnumItems **ppenum);
    STDMETHODIMP Skip(ULONG celt);
    STDMETHODIMP Reset();

    // Our methods
    STDMETHODIMP    Add(LPSYNCMGRITEM pelt);
    STDMETHODIMP    FindByID(SYNCMGRITEMID ItemID, LPSYNCMGRITEM *ppelt);
    DWORD           get_Count();

private:
    STDMETHODIMP    _Init();

};


#endif // #define _ENUMERATOR_CLASS_
