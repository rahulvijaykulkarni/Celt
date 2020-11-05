using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Movies
/// </summary>
public class Movies
{
	public Movies()
	{
		//
		// TODO: Add constructor logic here
		//

	}

    public string Name { get; set; }
    public int Year { get; set; }

    public string gstin { get; set; }
    public string ret_period { get; set; }
    public int txval { get; set; }
    public List<sup_details> listname { get; set; }
}