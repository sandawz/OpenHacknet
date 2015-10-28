﻿// Decompiled with JetBrains decompiler
// Type: Hacknet.FileSystem
// Assembly: Hacknet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 48C62A5D-184B-4610-A7EA-84B38D090891
// Assembly location: C:\Program Files (x86)\Steam\SteamApps\common\Hacknet\Hacknet.exe

using System;
using System.Xml;

namespace Hacknet
{
    internal class FileSystem
    {
        public Folder root;

        public FileSystem(bool empty)
        {
        }

        public FileSystem()
        {
            root = new Folder("/");
            root.folders.Add(new Folder("home"));
            root.folders.Add(new Folder("log"));
            root.folders.Add(new Folder("bin"));
            root.folders.Add(new Folder("sys"));
            generateSystemFiles();
        }

        public void generateSystemFiles()
        {
            var folder = root.searchForFolder("sys");
            folder.files.Add(new FileEntry(ThemeManager.getThemeDataString(OSTheme.HacknetTeal), "x-server.sys"));
            folder.files.Add(new FileEntry(Computer.generateBinaryString(500), "os-config.sys"));
            folder.files.Add(new FileEntry(Computer.generateBinaryString(500), "bootcfg.dll"));
            folder.files.Add(new FileEntry(Computer.generateBinaryString(500), "netcfgx.dll"));
        }

        public string getSaveString()
        {
            return "<filesystem>\n" + root.getSaveString() + "</filesystem>\n";
        }

        public static FileSystem load(XmlReader reader)
        {
            var fileSystem = new FileSystem(true);
            while (reader.Name != "filesystem")
                reader.Read();
            fileSystem.root = Folder.load(reader);
            return fileSystem;
        }

        public string TestEquals(object obj)
        {
            var fileSystem = obj as FileSystem;
            if (fileSystem == null)
                throw new ArgumentNullException();
            return root.TestEqualsFolder(fileSystem.root);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}