using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LinkNode
{
    public Dialogue m_data;
    public LinkNode m_next;

    public LinkNode()
    {
        m_data = null;
        m_next = null;
    }

    public LinkNode(Dialogue _m_data)
    {
        m_data = _m_data;
        m_next = null;
    }
}

public class LinkList
{
    public LinkNode header;
    public LinkNode last;

    public LinkNode Find(int ID)
    {
        LinkNode Current = new LinkNode();
        Current = header;
        while (!Dialogue.Equals(Current.m_data.dialogueID, ID) && Current.m_next != null)
        {
            Current = Current.m_next;
        }
        return Current;
    }

    public void Add(Dialogue a)
    {
        LinkNode n = new LinkNode();
        n.m_data = a;
        n.m_next = null;
        if (last == null)
        {
            header = n;
            last = n;
        }
        else
        {
            last.m_next = n;
            last = n;
        }
    }
}
