# Hair Salon

## Specification

| Behavior | Input | Output | Description of Specification |
| :-------------     | :------------- | :------------- | :------------- |
| Start with nothing in database to begin with.| | | Verify that there is nothing in the database as we don't want to mess anything up if the database is currently already in use. |
| Has the ability to add future stylists to the database. | "Bob the SuperStylist" | "Bob the SuperStylist" | Add stylist's name to the database. |
| Has the ability to update stylist's information in database. | "Bob the SuperStylist" | "Bob the DemotedStylist:(" | Update a stylist's existing information in the database. |
| Has the ability to remove stylists who no longer work at the salon. | "Bob the DemotedStylist" | NULL | Update a stylist's information in the database by removing them. |
| Has the ability to add future clients to the database. | "Jimmy the Client" | "Jimmy the Client" | Add a client to the database for future reference. |
| Has the abilty to update client/user information. | "Jimmy the Bad Client" | "Jimmy the Bad Client" | Update a client's existing information in the database. |
| Has the ability to remove clients that no longer go to the salon. | "Jimmy the Client" | NULL | Update a clients information in the database by removing them. |


## Installation/Prerequisites

Git Clone or Download at: https://github.com/eluts15/potential-hair-salon.git

In order to get server up and running, run the following command:

  dnx run

Now, navigate to localhost:5004

Runs on the .Net Framework

Requires Nancy Web Framework located at: http://nancyfx.org/. You can also do this via Windows PowerShell with the Command:

Install-Package Nancy

PowerShell may prompt you to download Nuget. Download this if necessary, as it is required to by Nancy.

All being said, the project also includes the project.lock.json so you can just use that I suppose.

Run dnu restore if necessary to update dependencies.

#TODO

1. Add the ability to update a particular client and/or stylist's information.
2. Using DATETIME allow for the ability to make appointments between a client and a particular stylist.

## Usage

1. Behavior Driven Development with the  Nancy Web Framework.
2. Working with SQL queries using SSMS.

## Known Bugs

1. None

## License

The MIT License

Copyright <2017> <Ethan Luts>

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
