﻿using Avalonia.Controls.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plc_soldier_avalonia.Models
{
    public class ApplicationLocalozation
    {
        /*
            0 - russian
            1 - english
        */
        public static Dictionary<string, List<string>> TopMenu = new Dictionary<string, List<string>>
        {
            {"File", new List<string>() { "Файл", "File" } },
            {"New project", new List<string>() { "Новый проект", "New project" } },
            {"Open project", new List<string>() { "Открыть проект", "Open project" } },
            {"Exit", new List<string>() { "Выход", "Exit" } },
            {"Edit", new List<string>() { "Редактировать", "Edit" } },
            {"View", new List<string>() { "Вид", "View" } },
            {"Logical organizer", new List<string>() { "Логический органайзер", "Logical organizer" } },
            {"Controller organizer", new List<string>() { "Контроллер-органайзер", "Controller organizer" } },
            {"Errors", new List<string>() { "Ошибки", "Errors" } },
            {"Search results", new List<string>() { "Результаты поиска", "Search results" } },
            {"Watch", new List<string>() { "Просмотр", "Watch" } },
            {"Central space", new List<string>() { "Центральная область", "Central space" } },
            {"Left bottom space", new List<string>() { "Левая нижняя область", "Left bottom space" } },
            {"Far right space", new List<string>() { "Крайняя правая область", "Far right space" } },
            {"Search", new List<string>() { "Поиск", "Search" } },
            {"Logic", new List<string>() { "Логика", "Logic" } },
            {"Communications", new List<string>() { "Коммуникации", "Communications" } },
            {"Tools", new List<string>() { "Инструменты", "Tools" } },
            {"Window", new List<string>() { "Окно", "Window" } },
            {"Help", new List<string>() { "Помощь", "Help" } },
        };

        public static int GetLanguageIndex(string language)
        {
            switch (language)
            {
                case "russian":
                    return 0;
                case "english":
                    return 1;
                default:
                    return -1;
            }
        } 
    }
}
