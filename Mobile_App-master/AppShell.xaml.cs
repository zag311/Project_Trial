﻿namespace Recraft
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
        }
    }
}
