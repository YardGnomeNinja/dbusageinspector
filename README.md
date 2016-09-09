## Synopsis

A Visual C# application that identifies references to SQL database objects and allows them to be written to a Neo4j graph database to visualize their relationships.

## Motivation

This was built initially to help identify database objects that were no longer in use in a system with over 7000 SQL objects.
It grew to incorporate identifying the relationships between objects.

## Installation

Compile with VS 2015 and run.

For Neo4j (3.0 or higher), [download and install the latest community edition](https://neo4j.com/download/) (3.0.4 was used for testing, 3.0 or newer required due to use of Bolt protocol)

* Run the Neo4j Community Edition application
* Select a folder to store the graph database instance
* Click the "browse to" link in the application to manage the instance
* Sign-in according to the page instructions and change password if required

## Example configuration values

Configuration settings will be loaded from and saved in "config.dbui" upon running and exiting the application respectively

* **Code Path:** ```C:\<workingdir>\<projectdir>```
* **SQL Script Path:** ```C:\<workingdir>\<projectdir>\scripts```
* **SQL Server Connection String:** ```Data Source=<sqlServer>;Integrated Security=False;User ID=<user>;Password=<password>;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False```
* **Neo4j URL:** ```bolt://localhost```
* **Neo4j Username:** ```neo4j```
* **Neo4j Password:** ```<password>```

## Use

There are two features in the application:

#### Compare SQL CREATE script files against existing SQL Server Objects to identify disparities
* *Searches a directory (and descendants)*
* *Only checks for the existence of the object based on its name, it does not compare the definition of the objects*

* **Required configuration settings:**
	* SQL Script Path
	* SQL Server Connection String

* **Directions**
	1. Click "Get SQL Script Objects"
	2. Click "Get SQL Server Objects"
	3. Click "Compare Lists"

#### Collect SQL Server object references in code and SQL Server to be written to a Neo4j graph database
* *Code matching designed for C# and VB.NET syntax, but should be fairly easy to configure for others*
* *SQL related matching designed for Microsoft SQL Server T-SQL syntax, but should be fairly easy to configure for others*
* *Currently only looks for references to TABLES, PROCEDURES, VIEWS, FUNCTIONS, TRIGGERS, and SYNONYMS (note: SYNONYMS do not currently identify their target)*
* *Currently identifies relationships as SELECTS_FROM (FROM and JOINS), INSERTS_INTO, UPDATES, DELETES_FROM, EXECUTES (PROCEDURES), CALLS (FUNCTIONS), REFERENCES (default when relationship could not be determined)*
* *Currently identifies ALL references from a source. For example, if a procedure contains multiple 'UPDATE \<tablename>' references, a relationship will be created for each*

* **Required configuration settings:**
	* Code Path
	* SQL Server Connection String
	* Neo4j URL
	* Neo4j Username
	* Neo4j Password

* **Directions:**
	1. Click "Get SQL Server Objects"
	2. Click "Get Code References To SQL Server Objects" (not required if only interested in SQL Server References)
	3. Click "Get SQL Server References" (not required if only interested in Code References)
	4. Click "Write to Neo4j"

**IMPORTANT:** There are currently no progress bars and large processes may take a while and the application will appear to hang. Be patient. Recent tests of our system yielded the following results: Approximately 1000 code files (in many cases thousands of lines long) took about 1 - 2 minutes to scan. Approximately 7000 SQL objects (also often thousands of lines long) took about 3 - 5 minutes to scan. Creating over 38,000 object and relationships took about 10 minutes to complete. Again, please be patient.

## Useful Cypher queries

Once you've written your database references to Neo4j, you'll probably want to run some queries against it. Here are a few basics. [And here's the documentation to build upon these](https://neo4j.com/docs/developer-manual/current/cypher/).

* Get any node with a desired 'name' property
	* ```MATCH (n {name: '<myname>'}) RETURN n;```

* Get a specific type of node via its label with a desired 'name' property
	* ```MATCH (n:TABLE {name: '<tablename>'}) RETURN n;```

* Get all nodes with no incoming or outgoing relationships
	* ```MATCH (n) WHERE NOT (n)--() RETURN n;```

* Get all stored procedures that update a specific table, return the table node as well
	* ```MATCH (p:PROCEDURE), (t:TABLE {name: '<tablename>'}) WHERE (p)-[:UPDATES]->(t) RETURN p,t;```

**Note:** You can double-click on any node to display any related nodes.

## Created and tested using

* Windows 10
* Microsoft Visual Studio Professional 2015
* Neo4j v3.0.4
* SQL Server 2005

## License

GNU GENERAL PUBLIC LICENSE v3