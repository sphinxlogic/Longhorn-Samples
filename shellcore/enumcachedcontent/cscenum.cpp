// cscenum.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <initguid.h>
#include "cscobj.h"


HRESULT ProcessEnumeratedObject(IOfflineFile *pFile);
HRESULT EnumerateFolderContent(IOfflineFile *pFolder);


void ReportOneFolder(IOfflineFile *pFile)
{
    LPWSTR pszPath;

    HRESULT hr = pFile->GetPath(&pszPath);
    if (SUCCEEDED(hr))
    {
        OFFLINEFILES_FILE_STATUS status;

        hr = pFile->GetStatus(&status);
        if (SUCCEEDED(hr))
        {
            if (OFFLINEFILES_FILE_STATUS_SHAREROOT & status)
            {
                wprintf(L"%s [SHARE]\n", pszPath);
            }
            else
            {
                wprintf(L"%s [DIR]\n", pszPath);
            }
        }
        CoTaskMemFree(pszPath);
    }
}
 

void ReportOneFile(IOfflineFile *pFile)
{

    LPWSTR pszPath;
    HRESULT hr = pFile->GetPath(&pszPath);
    if (SUCCEEDED(hr))
    {
        wprintf(L"%s\n", pszPath);
        CoTaskMemFree(pszPath);
    }
}


HRESULT ProcessEnumeratedObject(IOfflineFile *pFile)
{
    OFFLINEFILES_FILE_STATUS status;

    HRESULT hr = pFile->GetStatus(&status);
    if (SUCCEEDED(hr))
    {
        if (OFFLINEFILES_FILE_STATUS_DIRECTORY & status)
        {
            hr = EnumerateFolderContent(pFile);
            ReportOneFolder(pFile);
        }
        else
        {
            ReportOneFile(pFile);
        }
    }
    return hr;
}


HRESULT EnumerateFolderContent(IOfflineFile *pFolder)
{
    IEnumOfflineFiles *penumFiles;

    HRESULT hr = pFolder->EnumFiles(&penumFiles);
    if (SUCCEEDED(hr))
    {
        ULONG celtFetched;
        IOfflineFile *pFile;
        while (S_OK == (hr = penumFiles->Next(1, &pFile, &celtFetched)))
        {
            hr = ProcessEnumeratedObject(pFile);
            pFile->Release();
        }
        penumFiles->Release();
    }
    return hr;
}


HRESULT EnumerateCacheContent(IOfflineFilesCache *pCache)
{
    IEnumOfflineFiles *penumShares;

    HRESULT hr = pCache->EnumShareRoots(&penumShares);
    if (SUCCEEDED(hr))
    {
        IOfflineFile *pShare;
        ULONG celtFetched;
        while(S_OK == (hr = penumShares->Next(1, &pShare, &celtFetched)))
        {
            EnumerateFolderContent(pShare);
            ReportOneFolder(pShare);
            pShare->Release();
        }
        penumShares->Release();
    }
    return hr;
}



int _tmain(int argc, _TCHAR* argv[])
{
    HRESULT hr = CoInitialize(NULL);
    if (SUCCEEDED(hr))
    {
        IOfflineFilesCache *pCache;
        hr = CoCreateInstance(CLSID_OfflineFilesCache, 
                              NULL, 
                              CLSCTX_INPROC_SERVER, 
                              IID_IOfflineFilesCache, 
                              (void **)&pCache);
        if (SUCCEEDED(hr))
        {
            hr = EnumerateCacheContent(pCache);
            pCache->Release();
        }
        CoUninitialize();
    }
}







