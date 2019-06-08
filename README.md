# Shipment manager
A winform application Created for shipment company client using Devexpress tools - MS sql server database

The application works for many pcs by hosting it on local server

It is protected by serial number generator

# How it works
-The DataAccessLayer Class Has 4 ADO.NET methods that i created that take the sql command and parameters (if any) and fetchs the records 
from the database.

-Every class contains number of static methods that return boolean result if the operation on database is successful.

-That boolean is used inside the form backend code to check for results before they are viewed inside datagridviews or make various operations

-Using parameters every form is used twice (Adding or editing data).

# Reporting
Using devexpress reporting the client needed only 2 reports:

  1-One for viewing (الحوافظ) which was a template paper he used before buying the application
  
  2-one for advanced search which is a form used to search using criterias the client can specifiy
