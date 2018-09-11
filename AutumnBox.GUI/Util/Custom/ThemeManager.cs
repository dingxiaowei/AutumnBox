﻿/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 15:41:22 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.Properties;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AutumnBox.GUI.Util.Custom
{
    class ThemeManager : IThemeManager
    {
        private const int INDEX_OF_THEME = 2;
        private const string FILE_PATH = "pack://application:,,,/AutumnBox.GUI;component/Resources/Themes/";
        private const string THEME_NAME_KEY = "ThemeName";
        public static IThemeManager Instance { get; private set; }
        static ThemeManager()
        {
            Instance = new ThemeManager();
        }

        public IEnumerable<ITheme> Themes => themes;

        public ITheme Current
        {
            get => GetCurrentTheme();
            set
            {
                Apply(value);
            }
        }

        private List<ITheme> themes;
        private ThemeManager()
        {
            themes = new List<ITheme>() {
                ThemeImpl.LoadFrom("Autumn.xaml"),
            };
        }

        public void ApplyBySetting()
        {
            var settingTheme = Settings.Default.Theme;
            var findingResult = themes.Find(_the => _the.Name == settingTheme);
            Current = findingResult;
        }

        private ITheme GetCurrentTheme()
        {
            var currentThemeName = App.Current.Resources[THEME_NAME_KEY].ToString();
            return themes.Find((t) =>
            {
                return t.Name == currentThemeName;
            });
        }
        private void Apply(ITheme theme)
        {
            if (Current.Equals(theme))
            {
                return;
            }
            App.Current.Resources.MergedDictionaries[INDEX_OF_THEME] = theme.Resource;
        }

        private class ThemeImpl : ITheme
        {
            public string Name => Resource[THEME_NAME_KEY].ToString();

            public ResourceDictionary Resource { get; private set; }

            public static ThemeImpl LoadFrom(string filename)
            {
                var resouceDict = new ResourceDictionary { Source = new Uri(FILE_PATH + filename) };
                return new ThemeImpl()
                {
                    Resource = resouceDict,
                };
            }
        }
        private class RandomTheme : ITheme
        {
            public string Name => "Random-随机";

            public ResourceDictionary Resource => throw new NotImplementedException();
            public void Random() { }
        }
    }
}
