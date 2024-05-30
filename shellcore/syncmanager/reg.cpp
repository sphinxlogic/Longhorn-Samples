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

#define HANDLERITEMSKEY L"\\HandlerItems"
#define HANDLERVALUE_DISPLAYNAME    L"DisplayName"


//+---------------------------------------------------------------------------
//
//  Function:   SetRegKeyValue, private
//
//  Synopsis:   Internal utility function to set a Key, Subkey, and value
//              in the system Registry under HKEY_CLASSES_ROOT.
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

BOOL SetRegKeyValue(HKEY hKeyTop, LPTSTR pszKey, LPTSTR pszSubkey, LPTSTR pszValue)
{
    BOOL bOk = FALSE;
    LONG ec;
    HKEY hKey;
    WCHAR szKey[MAX_STRING_LENGTH];

    StringCchCopy(szKey, ARRAYSIZE(szKey), pszKey);

    if (NULL != pszSubkey)
    {
        StringCchCat(szKey, ARRAYSIZE(szKey), L"\\");
        StringCchCat(szKey, ARRAYSIZE(szKey), pszSubkey);
    }

    ec = RegCreateKeyEx(
            hKeyTop,
            szKey,
            0,
            NULL,
            REG_OPTION_NON_VOLATILE,
            KEY_READ | KEY_WRITE,
            NULL,
            &hKey,
            NULL);

    if (NULL != pszValue && ERROR_SUCCESS == ec)
    {
        ec = RegSetValueEx(
            hKey,
            NULL,
            0,
            REG_SZ,
            (BYTE *)pszValue,
            (lstrlen(pszValue)+1)*sizeof(TCHAR));

        if (ERROR_SUCCESS == ec)
            bOk = TRUE;

        RegCloseKey(hKey);
    }

  return bOk;
}

//+---------------------------------------------------------------------------
//
//  Function:   GetRegKeyValue, private
//
//  Synopsis:   Internal utility function to get a Key value
//              in the system Registry.
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

LRESULT GetRegKeyValue(HKEY hkeyParent, LPCTSTR pcszSubKey,
                                   LPCTSTR pcszValue, PDWORD pdwValueType,
                                   PBYTE pbyteBuf, PDWORD pdwcbBufLen)
{
    HKEY hkeySubKey;
    LONG lResult = RegOpenKeyEx(hkeyParent, pcszSubKey, 0, KEY_QUERY_VALUE,
                           &hkeySubKey);

    if (lResult == ERROR_SUCCESS)
    {
        lResult = RegQueryValueEx(hkeySubKey, pcszValue, NULL, pdwValueType,
                                  pbyteBuf, pdwcbBufLen);
        RegCloseKey(hkeySubKey);
    }

    return(lResult);
}

//+---------------------------------------------------------------------------
//
//  Function:   AddRegNamedValue, private
//
//  Synopsis:   Internal utility function to add a named data value to an
//              existing Key (with optional Subkey) in the system Registry
//              under HKEY_CLASSES_ROOT.
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

BOOL AddRegNamedValue(
       LPTSTR pszKey,
       LPTSTR pszSubkey,
       LPTSTR pszValueName,
       LPTSTR pszValue)
{
    BOOL bOk = FALSE;
    LONG ec;
    HKEY hKey;
    TCHAR szKey[MAX_STRING_LENGTH];

    StringCchCopy(szKey, ARRAYSIZE(szKey), pszKey);

    if (NULL != pszSubkey)
    {
        StringCchCat(szKey, ARRAYSIZE(szKey), L"\\");
        StringCchCat(szKey, ARRAYSIZE(szKey), pszSubkey);
    }

    ec = RegOpenKeyEx(
            HKEY_CLASSES_ROOT,
            szKey,
            0,
            KEY_READ | KEY_WRITE,
            &hKey);

    if (NULL != pszValue && ERROR_SUCCESS == ec)
    {
        ec = RegSetValueEx(
                hKey,
                pszValueName,
                0,
                REG_SZ,
                (BYTE *)pszValue,
                (lstrlen(pszValue)+1)*sizeof(TCHAR));
        if (ERROR_SUCCESS == ec)
            bOk = TRUE;
        RegCloseKey(hKey);
    }

    return bOk;
}



//+---------------------------------------------------------------------------
//
//  Function:   CreateHandlerPrefKey, private
//
//  Synopsis:   given a server clsid does work of openning up the
//              handler perf key
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

HKEY CreateHandlerPrefKey(CLSID CLSIDServer)
{
    TCHAR szFullKeyName[MAX_PATH];
    TCHAR szGUID[GUID_SIZE+1];
    HKEY hKey;

    StringFromGUID2(CLSIDServer, szGUID, GUID_SIZE);

    StringCchCopy(szFullKeyName, ARRAYSIZE(szFullKeyName), L"CLSID\\");
    StringCchCat(szFullKeyName, ARRAYSIZE(szFullKeyName), szGUID);
    StringCchCat(szFullKeyName, ARRAYSIZE(szFullKeyName), HANDLERITEMSKEY);

    // try to open handler items keyfs
    LONG lResult = RegCreateKeyEx(
            HKEY_CLASSES_ROOT,
            szFullKeyName,
            0,NULL,REG_OPTION_NON_VOLATILE,
            KEY_READ | KEY_WRITE,NULL,
            &hKey,NULL);

    return (ERROR_SUCCESS == lResult) ? hKey : NULL;
}

//+---------------------------------------------------------------------------
//
//  Function:   CreateHandlerItemPrefKey, private
//
//  Synopsis:   creates perf key for specified ItemID
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

HKEY CreateHandlerItemPrefKey(HKEY hkeyHandler,SYNCMGRITEMID ItemID)
{
    WCHAR szGUID[GUID_SIZE+1];
    HKEY hKeyItem;

    // try to open/create item key
    StringFromGUID2(ItemID, szGUID, GUID_SIZE);

    // try to open handler items keyfs
    LONG lResult = RegCreateKeyEx(
         hkeyHandler,
         szGUID,
         0,NULL,REG_OPTION_NON_VOLATILE,
         KEY_READ | KEY_WRITE,NULL,
         &hKeyItem,NULL);

    return (ERROR_SUCCESS == lResult) ? hKeyItem : NULL;
}


STDMETHODIMP Reg_SetUpRequestedDummySyncItems(DWORD dwci, CLSID CLSIDServer)
{
    WCHAR szName[MAX_PATH];

    // need to create some keys
    for (DWORD dw = 0; dw < dwci; ++dw)
    {
        ItemSettings itmstgs;

        StringCchPrintf(szName, ARRAYSIZE(szName), L"%s %d", L"Dummy Item", dw);

        // generate an ID for the item.
        CoCreateGuid(&itmstgs.ItemID);
        itmstgs.pszName = szName;
        Reg_SetItemSettingsForHandlerItem(CLSIDServer, itmstgs);
        //delete []itmstgs.pszName;
    }

    return NOERROR;
}


STDMETHODIMP Reg_SetItemSettingsForHandlerItem(CLSID CLSIDServer, ItemSettings itmstgs)
{
    WCHAR *pDisplayName;

    HKEY hKey = CreateHandlerItemPrefKey(CLSIDServer, itmstgs.ItemID);
    if (!hKey)
    {
        return E_UNEXPECTED;
    }

    // write out our info.
    RegSetValueEx(hKey, HANDLERVALUE_DISPLAYNAME,
           0,
           REG_SZ,
           (BYTE *) itmstgs.pszName,
           (wcslen(itmstgs.pszName) + 1) * sizeof(WCHAR));

    RegCloseKey(hKey);

    return NOERROR;
}

HKEY CreateHandlerItemPrefKey(CLSID CLSIDServer, SYNCMGRITEMID ItemID)
{
    WCHAR szGUID[GUID_SIZE+1] = {0};
    LONG lResult = -1;
    HKEY hKeyItem;

    HKEY hkeyHandler = CreateHandlerPrefKey(CLSIDServer);

    if (hkeyHandler)
    {
        // try to open/create item key
        StringFromGUID2(ItemID, szGUID, GUID_SIZE);
        
        // try to open handler items keyfs
        lResult = RegCreateKeyEx(
                    hkeyHandler,
                    szGUID,
                    0,NULL,REG_OPTION_NON_VOLATILE,
                    KEY_READ | KEY_WRITE,NULL,
                    &hKeyItem,NULL);

        RegCloseKey(hkeyHandler);
    }

  return (ERROR_SUCCESS == lResult) ? hKeyItem : NULL;

}

void Reg_DeleteHandlerPrefKey(CLSID CLSIDServer)
{
    WCHAR szGUID[GUID_SIZE+1] = {0};
    StringFromGUID2(CLSIDServer, szGUID, GUID_SIZE);
    WCHAR szHandlerKeyPath[GUID_SIZE + 10] = {0};
    StringCchPrintf(szHandlerKeyPath, ARRAYSIZE(szHandlerKeyPath), L"%s%s", L"\\CLSID\\", szGUID);
    SHDeleteKey(HKEY_CLASSES_ROOT, szHandlerKeyPath);
}

