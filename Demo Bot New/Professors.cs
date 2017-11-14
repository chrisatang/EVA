using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class Professors
{
    private static Dictionary<string, List<string>> professors;
    public static int professor_count;

    public Professors()
    {
        if (professors == null || professors.Count == 0)
            Initialize_professors();
    }

    private void Initialize_professors()
    {
        professors = new Dictionary<string, List<string>>();
        //Add professors here
        List<string> tmp = new List<string> { "csci 1", "rcos" };
        professors.Add("turner", tmp);
        ++professor_count;

        List<string> tmp1 = new List<string> { "rcos", "operating systems", "foundations of computer science" };
        professors.Add("goldschmidt", tmp1);
        ++professor_count;

        List<string> tmp2 = new List<string> { "rcos", "open source software" };
        professors.Add("krishnamoorthy", tmp2);
        ++professor_count;
    }

    public int get_professor_count()
    { return professor_count; }

    public List<string> get_keys()
    {
        List<string> tmp = new List<string>(professors.Keys);
        return tmp;
    }

    public List<string> get_teachers(string class_)
    {
        List<string> teachers = new List<string>();
        foreach (var prof in professors)
        {
            if (is_teacher(prof.Value, class_) == true)
            {
                teachers.Add(prof.Key);
            }
        }
        return teachers;
    }

    bool is_teacher(List<string> classes, string class_)
    {
        foreach (var class_taught in classes)
        {
            if (class_taught == class_)
                return true;
        }
        return false;
    }

    public int get_num_classes_taught(string name)
    {
        string target_key = "";
        foreach (var key in professors.Keys)
            if (key.ToLower().Equals(name.ToLower()))
            { target_key = key; }
        List<string> test;
        if (professors.TryGetValue(target_key, out test))
            return test.Count;
        else
            return 0;
    }
    public List<string> get_classes(string name)
    {
        List<string> test;
        if (professors.TryGetValue(name.ToLower(), out test))
            return test;
        else
            return null;
    }
}