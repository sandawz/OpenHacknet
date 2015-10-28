﻿// Decompiled with JetBrains decompiler
// Type: Hacknet.UIUtils.LCG
// Assembly: Hacknet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 48C62A5D-184B-4610-A7EA-84B38D090891
// Assembly location: C:\Program Files (x86)\Steam\SteamApps\common\Hacknet\Hacknet.exe

using System;
using System.Collections.Generic;

namespace Hacknet.UIUtils
{
    public class LCG
    {
        private int _state;

        public LCG(bool microsoft = true)
        {
            _state = (int) DateTime.Now.Ticks;
            Microsoft = microsoft;
        }

        public LCG(int n, bool microsoft = true)
        {
            _state = n;
            Microsoft = microsoft;
        }

        public bool Microsoft { get; set; }

        public bool BSD
        {
            get { return !Microsoft; }
            set { Microsoft = !value; }
        }

        public void reSeed(int seed)
        {
            _state = seed;
        }

        public int Next()
        {
            if (BSD)
                return _state = 1103515245*_state + 12345 & int.MaxValue;
            return ((_state = 214013*_state + 2531011) & int.MaxValue) >> 16;
        }

        public float NextFloat()
        {
            return Next()/(float) int.MaxValue;
        }

        public bool Flip()
        {
            if (BSD)
                return (_state = 1103515245*_state + 12345 & int.MaxValue) > 1073741823;
            return ((_state = 214013*_state + 2531011) & int.MaxValue) >> 16 > 1073741823;
        }

        public IEnumerable<int> Seq()
        {
            while (true)
                yield return Next();
        }
    }
}