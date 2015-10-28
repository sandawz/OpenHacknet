﻿// Decompiled with JetBrains decompiler
// Type: Hacknet.Mission.DeathRowRecordModifyMission
// Assembly: Hacknet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 48C62A5D-184B-4610-A7EA-84B38D090891
// Assembly location: C:\Program Files (x86)\Steam\SteamApps\common\Hacknet\Hacknet.exe

using System.Collections.Generic;

namespace Hacknet.Mission
{
    internal class DeathRowRecordModifyMission : MisisonGoal
    {
        public Folder container;
        public Computer deathRowDatabase;
        public string fname;
        public string lastWords;
        public string lname;
        public OS os;

        public DeathRowRecordModifyMission(string firstName, string lastName, string lastWords, OS _os)
        {
            os = _os;
            fname = firstName;
            lname = lastName;
            this.lastWords = lastWords;
            var computer = Programs.getComputer(os, "deathRow");
            deathRowDatabase = computer;
            container = computer.getFolderFromPath("dr_database/Records", false);
        }

        public override bool isComplete(List<string> additionalDetails = null)
        {
            var recordForName =
                ((DeathRowDatabaseDaemon) deathRowDatabase.getDaemon(typeof (DeathRowDatabaseDaemon))).GetRecordForName(
                    fname, lname);
            if (recordForName.RecordNumber == null)
                return false;
            if (lastWords == null)
                return true;
            var str1 = recordForName.Statement.ToLower().Replace("\r", "").Replace(",", "").Replace(".", "");
            var str2 = lastWords.ToLower().Replace("\r", "").Replace(",", "").Replace(".", "");
            if (str1.Contains(str2) || str1 == str2 || !str1.StartsWith(str2))
                ;
            return true;
        }
    }
}