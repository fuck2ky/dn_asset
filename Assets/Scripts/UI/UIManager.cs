﻿using System.Collections.Generic;
using UnityEngine;

public class UIManager : XSingleton<UIManager>
{

    //放入堆栈的
    private Stack<IUIDlg> m_stack;

    //不放入堆栈的
    private List<IUIDlg> m_list;

    //回收的dlg 从栈中弹出的，暂时隐藏不显示的
    private Dictionary<uint, IUIDlg> m_recycle;

    private Camera _uiCamera;
    private Canvas _canvas;
    private int _sort = 100;
    private const int _gap = 10;
    private const int _top = 800000;


    public static int _far_far_away = 1000;
    public static Vector3 Far_Far_Away = new Vector3(10000, 10000, 0);

    public Camera UiCamera { get { return _uiCamera; } }

    public Canvas Canvas { get { return _canvas; } }

    public void Initial()
    {
        m_stack = new Stack<IUIDlg>();
        m_list = new List<IUIDlg>();
        LoadRoot();
    }


    public void UnInitial()
    {
        if (m_stack != null)
            m_stack.Clear();
        if (m_list != null)
            m_list.Clear();
    }


    private void LoadRoot()
    {
        GameObject go = XResourceMgr.Load<GameObject>("UI/UIRoot", AssetType.Prefab);
        GameObject.DontDestroyOnLoad(go);
        _uiCamera = go.GetComponent<Camera>();
        _canvas = go.transform.GetChild(0).GetComponent<Canvas>();
        Debug.Log("camera：" + _uiCamera.name + " canvas:" + _canvas.name);
    }

    private bool Exist(IUIDlg dlg)
    {
        if (dlg.pushStack)
        {
            var e = m_stack.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.id == dlg.id)
                    return true;
            }
        }
        else
        {
            var e2 = m_list.GetEnumerator();
            while (e2.MoveNext())
            {
                if (e2.Current.id == dlg.id)
                    return true;
            }
        }
        return false;
    }


    public void LoadDlg(IUIDlg dlg)
    {
        if (!Exist(dlg))
        {
            if (m_recycle.ContainsKey(dlg.id))
            {
                m_recycle.Remove(dlg.id);
                dlg.innerBehaviour.gameObject.SetActive(true);
            }
            else
            {
                Object o = XResourceMgr.Load<GameObject>(dlg.fileName, AssetType.Prefab);
                GameObject go = GameObject.Instantiate(o) as GameObject;
                DlgBehaviourBase behav = go.AddComponent<DlgBehaviourBase>();
                dlg.SetBehaviour(behav);
                dlg.OnLoad();
            }
            if (dlg.pushStack)
            {
                m_stack.Push(dlg);
                _sort = dlg.innerBehaviour.SortDepth(_sort) + _gap;
                dlg.OnShow();
            }
            else m_list.Add(dlg);
        }
        if (!dlg.pushStack) //不在栈中的ui 每次调用的时候都展示在最前 不管之前是否cache
        {
            if (dlg.type == DlgType.Top)
                dlg.innerBehaviour.SortDepth(_top);
            else if (dlg.type == DlgType.Surface)
                _sort = dlg.innerBehaviour.SortDepth(_sort) + _gap;

            dlg.OnShow();
        }
    }

    public bool Hide(IUIDlg dlg)
    {
        if (Exist(dlg))
        {
            if (dlg.pushStack)
            {
                var v = m_stack.Peek();
                if (v.id == dlg.id)
                {
                    HideDlg(dlg);
                    m_stack.Pop();
                }
            }
            else
            {
                for (int max = m_list.Count, i = max - 1; i >= 0; i--)
                {
                    if (m_list[i].id == dlg.id)
                    {
                        HideDlg(dlg);
                        m_list.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        return true;
    }


    private void HideDlg(IUIDlg dlg)
    {
        dlg.OnHide();
        dlg.innerBehaviour.gameObject.SetActive(false);
        m_recycle[dlg.id] = dlg;
    }

    public bool Unload(IUIDlg dlg)
    {
        if (Exist(dlg))
        {
            if (dlg.pushStack)
            {
                var v = m_stack.Peek();
                if (v.id == dlg.id)
                {
                    DestroyDlg(dlg);
                    m_stack.Pop();
                }
                else
                {
                    Debug.LogError("dlg is not in stack " + dlg.fileName);
                }
            }
            else
            {
                for(int i=0,max=m_list.Count;i<max;i++)
                {
                    if(m_list[i].id==dlg.id)
                    {
                        DestroyDlg(dlg);
                        m_list.RemoveAt(i);
                        break;
                    }
                }
            }
            return true;
        }
        return false;
    }


    private void DestroyDlg(IUIDlg dlg)
    {
        dlg.OnDestroy();
        GameObject.Destroy(dlg.innerBehaviour.gameObject);
        XResourceMgr.UnloadAsset(dlg.fileName, AssetType.Prefab);
    }


    public void OnEnterScene()
    {
        _sort = 0;
    }
}