[ { 
	"Duration" : 9,
    "Id" : 1,
    "Name" : "Firefox Stable",
    "PercentDone" : 40,
    "StartDate" : "2012-05-01",
    "expanded" : true,
	"children" : [
		{ 
			"Duration" : 4,
			"Id" : 11,
            "leaf" : true,
			"Name" : "UI Fixes",
			"PercentDone" : 30,
			"StartDate" : "2012-05-01"
		},
		{ 
			"Duration" : 3,
			"Id" : 12,
			"leaf" : true,
			"Name" : "Tests",
			"PercentDone" : 0,
			"StartDate" : "2012-05-08"
		},
		{ 
			"Duration" : 1,
			"Id" : 13,
			"leaf" : true,
			"Name" : "Documentation",
			"PercentDone" : 40,
			"StartDate" : "2012-05-11"
		},
		{ 
			"Duration" : 0,
			"Id" : 17,
			"leaf" : true,
			"Name" : "Release",
			"PercentDone" : 0,
			"StartDate" : "2012-05-14"
		}
	]
  },
  { 
	"Duration" : 14,
	"Id" : 4,
	"Name" : "Firefox Beta",
	"PercentDone" : 50,
	"StartDate" : "2012-05-05",
    "expanded" : true,
	"children" : [{ 
			"Duration" : 6,
			"Id" : 34,
			"leaf" : true,
			"Name" : "Rendering bugs",
			"PercentDone" : 0,
			"StartDate" : "2012-05-05"
		},
		{ 
			"Duration" : 5,
			"Id" : 14,
			"leaf" : true,
			"Name" : "Addons tests",
			"PercentDone" : 30,
			"StartDate" : "2012-05-07"
		},
		{ 
			"Duration" : 7,
			"Id" : 15,
			"Name" : "Tests",
			"PercentDone" : 40,
			"StartDate" : "2012-05-15",
			"expanded" : true,
			"children" : [
				{ 
					"Duration" : 4,
					"Id" : 20,
					"leaf" : true,
					"Name" : "Rendering Tests",
					"PercentDone" : 30,
					"StartDate" : "2012-05-15"
				},
				{ 
					"Duration" : 4,
					"Id" : 19,
					"leaf" : true,
					"Name" : "UI Tests",
					"PercentDone" : 40,
					"StartDate" : "2012-05-18"
				}
			]
		},
		{ 
			"Duration" : 0,
			"Id" : 30,
			"leaf" : true,
			"Name" : "Release",
			"PercentDone" : 0,
			"StartDate" : "2012-05-24"
		}					
	]
  },
	{ 
		"Duration" : 14,
		"Id" : 6,
		"Name" : "Firefox Nightly",
		"PercentDone" : 50,
		"StartDate" : "2012-05-15",
        "expanded" : true,
		"children" : [
			{ 
				"Duration" : 8,
				"Id" : 25,
				"leaf" : true,
				"Name" : "Improve memory management",
				"PercentDone" : 10,
				"StartDate" : "2012-05-16"
			  },
			  { 
			  	"Duration" : 4,
				"Id" : 26,
				"leaf" : true,
				"Name" : "Test developer tools",
				"PercentDone" : 20,
				"StartDate" : "2012-05-15"
			  },
			  { 
			  	"Duration" : 3,
				"Id" : 27,
				"leaf" : true,
				"Name" : "Test new theme",
				"StartDate" : "2012-05-29"
			  }
		]
	},
	{ 
		"Duration" : 12,
		"Id" : 8,
		"Name" : "Boot2Gecko",
		"PercentDone" : 40,
		"StartDate" : "2012-05-22",
        "expanded" : true,
		"children" : [
			{ 
				"Duration" : 3,
				"Id" : 22,
				"leaf" : true,
				"Name" : "Homescreen",
				"PercentDone" : 50,
				"StartDate" : "2012-05-22"
			},
			{ 
				"Duration" : 6,
				"Id" : 23,
				"leaf" : true,
				"Name" : "Firefox Mobile",
				"PercentDone" : 20,
				"StartDate" : "2012-05-22",
			},
			{ 
				"Duration" : 6,
				"Id" : 24,
				"leaf" : true,
				"Name" : "Touch events tests",
				"PercentDone" : 50,
				"StartDate" : "2012-05-31"
			}
		]
	}
]