### Sample Project with N layered domain driven design architecture.
### Contains Dependency injection, Entity Framework Hybrid model(Code First + Database First) pattern.

## Developed By:
``````
Brijesh Patel

Entity Framework: Version 6.1.3
Unity: Version 4.0.1
Unity.MVC: Version 4.0.1
``````
This Example is using Hybrid Model for ORM.
Here tabels, Stored procedures are created in database, then they are map with configuration mannually.

## Stored Procedure Mapping Example:
``````javascript
public partial class DatabaseContext : IDatabaseContext
{
	public IEnumerable<CustomerOrderHistory> CustomerOrderHistory(string customerID)
	{
		var customerIDParameter = customerID != null ?
			new SqlParameter("@CustomerID", customerID) :
			new SqlParameter("@CustomerID", typeof (string));

		return Database.SqlQuery<CustomerOrderHistory>("CustOrderHist @CustomerID", customerIDParameter);
	}
}
``````
Note: Here DatabaseContext class is a partial class. And CustOrderHist is name of stored procedure you want to pass.


## Specifications basic:
``````javascript
public static class QuerySpecifications(){
	public virtual Expression<Func<TEntity, bool>> Query(int id)
	{
		return Expression<Func<TEntity, bool>>(a=>a.Id==id);
	}
}
``````

## Utility (Demo.Common.Utils):
``````
1. CustomCrypto class:
	Provides methosd for encypting and decrypting text.
2. XmlSerializer class:
	Provides methods to serializa and deserialize object and xml string, also from file.
``````

## Extended classes (Demo.Common.ExtendedClass):
``````
1. BaseController Class:
	Base controller class to inherit by controllers in MVC.
	Filter Attributes are added.
	Also extends controller class of System.Web.MVC.
2. CustomJsonResult Class:
	Extends ActionResult class.
	Used to return data in JSON format.
``````

## Fault Exception in WCF
``````
1. Add  [FaultContract(typeof(ServiceException))] on contract
2. Add fault using below code
	
	throw new FaultException<ServiceException>(ExceptionManager.
                    HandleException("Product ID does not exist in system."));

	throw new FaultException<ServiceException>(ExceptionManager.
                    HandleException(ex));
``````
Note: Exception manager itself log exception details. So no need to add logs in WCF methods.


## Handling Session Authentication in Javascript side:
``````javascript
$(document).ajaxError(function (e, xhr) {
	if (xhr.status == 401)
		window.location = "/Account/Login";
	else if (xhr.status == 403)
		alert("You have no enough permissions to request this resource.");
});
``````

## SQL Server Database type	to .NET Framework type mapping

SQL Server Database type | .NET Framework type 
------------ | -------------
Bigint | Int64 
binary, varbinary | Byte[] 
Bit | Boolean 
date, datetime, datetime2, smalldatetime | DateTime 
Datetimeoffset | DateTimeOffset 
decimal, money, smallmoney, numeric | Decimal 
float | Double 
int| Int32 
nchar, nvarchar,, char, varchar | String
real | Single 
rowversion, timestamp | Byte[] 
smallint | Int16 
time | TimeSpan 
tinyint | Byte 
uniqueidentifier | Guid
