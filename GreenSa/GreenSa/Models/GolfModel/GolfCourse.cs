﻿using GreenSa.Models.Tools;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using SQLiteNetExtensions.Attributes;
using SQLite;

namespace GreenSa.Models.GolfModel
{
    public class GolfCourse
    {
        public string NameCourse { get; set; }

        [PrimaryKey]
        public string Name { get; set; }


        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Hole> Holes { get;   set; }
        

        public GolfCourse()
        {
        }
        public GolfCourse(string name,string nameCourse,List<Hole> holes)
        {
            this.Name = name;
            this.Holes = holes;
            this.NameCourse = nameCourse;
        }

        public List<Hole>.Enumerator GetHoleEnumerator()
        {
            return Holes.GetEnumerator();
        }

        public override string ToString()
        {
            String str= Name+", "+ NameCourse + " { "+Holes.Count+"\n";
            foreach (Hole m in Holes)
                str += m.ToString() + " \n";
            str += "}";
            return str;
        }
        public override bool Equals(object obj)
        {
            return obj is GolfCourse &&  (obj as GolfCourse).Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }


    }
}