using System.Collections.Concurrent;

namespace mvccore.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}


public class Database
{
    public Database()
    {
        users = new List<User>();
        timesheet = new List<Clocking>();

        users.Add(new User { username = "farge", name = "La Farge" });
        timesheet.Add(new Clocking { username = "farge", start = DateTime.Now.AddHours(-1), end = null });
    }
    public List<User> users { get; set; }

    public List<Clocking> timesheet { get; set; }

}

public class ConcurrentDatabase
{
    private static readonly ConcurrentDictionary<string, User> users
        = new ConcurrentDictionary<string, User>();

    private static readonly ConcurrentDictionary<string, Clocking> timesheet
    = new ConcurrentDictionary<string, Clocking>();

    // public Job Get(string key)
    // {
    //     return _jobs.GetOrAdd(key, CreateNewJob());
    // }

}

public class TimesheetViewModel
{

    public User user { get; set; }

    public Clocking sheet { get; set; }

}

public class ClockViewModel
{
    public string username { get; set; }

}

public class User
{
    public string username { get; set; }
    public string name { get; set; }
}

public class Clocking
{
    public string username { get; set; }
    public DateTime start { get; set; }
    public DateTime? end { get; set; }
}
