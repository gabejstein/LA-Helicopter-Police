using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public List<Menu> menus = new List<Menu>();
    Stack<Menu> menuStack = new Stack<Menu>();
    Menu current;

    public static MenuController singleton;

    void Awake()
    {
        if(singleton==null)
        {
            singleton = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(menus.Count>0)
        {
            current = menus[0];
            menuStack.Push(menus[0]);
            UpdateMenus();
        }
    }

    Menu FindMenu(string id)
    {
        foreach(Menu menu in menus)
        {
            if (menu.id == id)
                return menu;
        }

        return null;
    }

    public void GoToMenu(string id)
    {
        current = FindMenu(id);
        menuStack.Push(current);
        UpdateMenus();
    }

    public void GoBackOneMenu()
    {
        if(menuStack.Count ==1)
        {
            Debug.Log("No menus to go back to");
            return;
        }

        menuStack.Pop();
        current = menuStack.Peek();
        UpdateMenus();
    }

    private void UpdateMenus() //Makes only the current menu the active gameobject.
    {
        foreach(Menu menu in menus)
        {
            if(menu==current)
            {
                menu.menuUI.SetActive(true);
            }
            else
            {
                menu.menuUI.SetActive(false);
            }
        }
    }
}
