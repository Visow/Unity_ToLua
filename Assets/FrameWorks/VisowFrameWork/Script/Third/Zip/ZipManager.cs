// -*- coding: utf-8; tab-width: 4 -*-
using UnityEngine;
using System;
using System.Collections;

//zip manager
public class ZipManager : MonoBehaviour
{
	private ZipProxy m_zipProxy = null;

	public float Progress {
		get {
			if (this.m_zipProxy != null) {
				return this.m_zipProxy.decompressCount * 1f / this.m_zipProxy.totalCount;
			}
			return 0;
		}
	}

	void Awake ()
	{
		StartCoroutine ("ZipUpdate");
	}
		
	// Update is called once per frame
	IEnumerator ZipUpdate ()
	{
		while (true) {
			ZipProxy.checkoutZipProxy ();
			yield return new WaitForSeconds (0.33f);
		}
	}

	public ZipProxy uncompless (string zipFile, string extralPath,
	                            System.Action<object> endCallback, System.Action<Exception> errorCallback = null)
	{
		this.m_zipProxy = ZipProxy.uncompless (zipFile, extralPath, endCallback, errorCallback);
		return this.m_zipProxy;
	}
}