/*
 * waveIDE: the libre code editor
 * Copyright (C) 2026  Ocean Apocalypse
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 * 
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;

using Avalonia;
using Avalonia.Controls;

using OceanApocalypse.Wave.IDE.UI.ViewModels;

namespace OceanApocalypse.Wave.IDE.UI.Views;

public partial class MainView : DrawerPage
{
    public MainView() => InitializeComponent();

    protected override async void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        UpdatePage(DrawerList.SelectedIndex);
    }

    private async void DrawerList_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ContentPage != null && sender is ListBox listbox)
        {
            var index = listbox.SelectedIndex;
            UpdatePage(index);
        }
    }

    private void UpdatePage(int index)
    {
        ViewModelBase page = index switch
        {
            0 => new HomeViewModel(),
            1 => new SettingsViewModel(),
            _ => throw new NotImplementedException()
        };

        ContentPage.Content = page;
        IsOpen = false;
    }
}
