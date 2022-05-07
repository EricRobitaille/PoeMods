using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;
using System.Linq;

public class StartScene : MonoBehaviour
{
    const string k_BigTable = "Big Table";

    [SerializeField] Button m_GetFileButton;
    [SerializeField] Button m_DownloadButton;
    [SerializeField] Button m_ParseFileButton;

    File m_File;
    GoogleDriveRequest m_CurrentRequest;

    void Awake()
    {
        m_DownloadButton.interactable = false;
        m_ParseFileButton.interactable = false;
    }

    public void GetFile()
    {
        GoogleDriveFiles.List().Send().OnDone += OnGetFileDone;
    }

    void OnGetFileDone(FileList fileList)
    {
        m_File = fileList.Files.FirstOrDefault(x => x.Name == k_BigTable);
        if (m_File != null)
        {
            m_DownloadButton.interactable = true;
        }
        else
        {
            Debug.LogError($"Could not locate file {k_BigTable}");
        }
    }

    public void DownloadFile()
    {
        if (m_File != null)
        {
            // Debug.Log(m_File.MimeType);
            GoogleDriveFiles.Export(m_File.Id, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet").Send().OnDone += OnFileDownloadDone;
        }
        else
        {
            Debug.Log("There is no file to download.");
        }
    }

    void OnFileDownloadDone(File file)
    {
        m_File = file;
        m_ParseFileButton.interactable = true;
    }

    public void ParseFile()
    {
        if (m_File != null)
        {
            var x = m_File.Content;
            Debug.Log(x);
        }
        else
        {
            Debug.Log("There is no file to parse.");
        }
    }
}
