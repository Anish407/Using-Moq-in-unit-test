<h1>Notes</h1>
1. If we dont specify the value for a property in the mocked object it will return its default
values. Int = 0, reference types = null and so on. To change this behavior

mockObject.DefaultValue = DefaultValue.Mock 
(this will create a mock object for the type instead of returning a null)

This will only work on interfaces, abstract or non sealed class (wont work on strings)

TrackingChangesInAMockedobject() 

2. To make sure that changes made to this property of the mock object are retained configure
the mock as shown below

    mockValidator.SetupProperty(i => i.AgeDiscount);

use this method to setup all properties, this will overwrite the previously setup
property. The code shown below will ensure that all the properties retain their values when updated

     mockValidator.SetupAllProperties();
Check TrackingChangesInAMockedobject()
