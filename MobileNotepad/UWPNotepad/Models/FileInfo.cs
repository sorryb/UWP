﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UWPNotepad.Models
{
    public class FileInfo
    {
        public StorageFile Ref { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public string Path { get; set; }

    }
}
