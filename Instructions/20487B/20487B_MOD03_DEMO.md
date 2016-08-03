# Module 3: Creating and Consuming ASP. NET Web API Services

# Lesson 2: Creating an ASP. NET Web API Service

### Demonstration: Creating Your First ASP. NET Web API Service

#### Demonstration Steps

1. In the **20487B-SEA-DEV-A** virtual machine, on the Start screen, click the **Visual Studio 2012** tile.
2. On the File menu, point to **New** , and then click **Project**.
3. In the **New Project** dialog box in the left-side of the navigation pane, expand the **Installed** node, expand the **Templates** node, expand the **Visual C#** node, click the **Web** node, and then click **NET MVC 4 Web Application** from the list of template.
4. In the **Name** text box, type **MyApp**.
5. In the **Location** text box, type **D:\Allfiles\Mod03\Democode\FirstWebApiService\Begin** , and then click **OK**.
6. In the **New ASP.NET MVC 4 Project** dialog box, click **Web API** , and then click **OK**.
7. In **Solution Explorer** , under the **MyApp** project, expand the **App\_Start** folder, and then double-click **cs**.
8. Locate the **MapHttpRoute** method call and explain the different parameters that are used in the method call.
9. In **Solution Explorer** , under the **MyApp** project, expand the **Controllers** folder, and then double-click **cs**.
10. Locate the parameterless **Get** action method, and discuss with the student how this method can be invoked by using HTTP (for example, using the _/api/values_ relative URI).
11. To start the application without debugging, press Ctrl+F5. The Internet Explorer 10 browser will open.
12. In the address bar of the browser, append the **api/values** tothe end of the address, and then press Enter.
13. When prompted by the browser, click **Open,** click **Try an app on this PC** , and then click **Notepad** to open the **json** file in Notepad.
14. Return to Visual Studio 2012.
15. In the **ValuesController** class, locate the parameterless **Get** action method, and change its code to the following.

	```cs
        [ActionName(&quot;List&quot;)]  
        public IEnumerable&lt;string&gt; Get()  
        {  
              return new string[] { &quot;value1&quot;, &quot;value2&quot; };  
        } 
```

16. Press Ctrl+S to save the changes.
17. In Solution Explorer, double-click **cs** under **App\_Start** folder.
18. Add the following code to the beginning of the **Register** method.

	```cs
        config.Routes.MapHttpRoute(  
             name: &quot;ActionApi&quot;,  
             routeTemplate: &quot;api/{controller}/{action}/{id}&quot;,  
             defaults: new { id = RouteParameter.Optional }  
        );
```

19. Press Ctrl+S to save the changes.
20. To start debugging the application, press **F5**.
21. In the address bar of the browser, append the **api/values/list** tothe end of the address, and then press Enter.
22. When prompted by the browser, click **Open** ,and then click **Notepad** to open the **json** file in **Notepad**.



# Lesson 3: Handling HTTP Requests and Responses

### Demonstration: Throwing Exceptions

#### Demonstration Steps

1. On the Start screen, click the **Visual Studio 2012** tile.
2. On the **File** menu, point to **Open** , and then click **Project/Solution**.
3. In the **File Name** text box, type **D:\Allfiles\Mod03\Democode\ThrowHttpResponseException\Begin\start.sln** , and then click **Open**.
4. In **Solution Explorer** , under the **Start** project, expand the **Controllers** folder, and double-click **cs**.
5. Locate the **Get** method call and explain how **HttpResponseMessage** is used to control errors.
6. Change the signature of the **Get** method to the following signature.

        public Destination Get(int id)
    
7. Remove the if-else statement and return the **destination** variable. When you are finished, the **Get** method should look like this.

        public Destination Get(int id)  
        {  
           var destination = \_destinations.Where(d=&gt;d.Id == id).FirstOrDefault();  
           return destination;  
        }

8. Discuss what should be the result of such a call (404 not found status). Show the students that the string **null** is being returned instead (with 200 OK status).
9. Change the **Get** method to validate the existence of a destination before sending a response. When you are finished, the **Get** method should look like this.

        public Destination Get(int id)  
        {  
           var destination = \_destinations.Where(d=&gt;d.Id == id).FirstOrDefault();    
       
           if (destination == null)    
              throw new HttpResponseException(    
                 new HttpResponseMessage(HttpStatusCode.NotFound));    
           return destination;    
        }    

10. Press Ctrl+S to save the changes.
11. To start the application without debugging, press Ctrl+F5. The Internet Explorer 10 browser will open.
12. In the address bar of the browser, append the **api/destinations/1** tothe end of the address, and then press Enter.
13. When prompted by the browser, click **Open** , and open the file in Notepad. Verify that you see information for Seattle.
14. Close Notepad and return to the browser. Append **api/destinations/6** tothe end of the address, and then press Enter. Verify that you get an HTTP 404 response.

# Lesson 4: Hosting and Consuming ASP.NET Web API Services

### Demonstration 1: Consuming Services Using JQuery

#### Demonstration Steps

1. On the Start screen, click the **Visual Studio 2012** tile.
2. On the File menu, point to **Open** , and then click **Project/Solution**.
3. In the **File Name** box, type **D:\Allfiles\Mod03\Democode\ConsumingFromJQuery\Begin\JQueryClient\JQueryClient.sln** , and then click **Open**.
4. In **Solution Explorer** , under the **JQueryClient** project, expand the **Views** folder, then expand the **Home** folder and double-click **cshtml**.
5. Locate the script section and explain jQuery retrieves the data from the server and how this method of work enables AJAX.
6. In the beginning of the script section, add jQuery code to register to the submit event of the **deleteLocation** form. The registration code should look as follows.

        $(&quot;#deleteLocation&quot;).submit(function (event) {  
              // this prevents the form from submitting  
            event.preventDefault();  
        });  

7. Add code retrieving the value of the **LocationId** input, and then create a delete call to the destination resource by using the jQuery **AJAX** function. The registration code should look as follows.

        $(&quot;#deleteLocation&quot;).submit(function (event) {  
               // this prevents the form from submitting  
            event.preventDefault();    
            
            var desId = $(this).find(&#39;input[name=&quot;LocationId&quot;]&#39;).val();  
       
            $.ajax({  
                type: &#39;DELETE&#39;,  
                url: &#39;destinations/&#39; + desId  
            });  
        });  
    
8. Press Ctrl+S to save the changes.
9. In **Solution Explorer** , under the **JQueryClient** project, expand the **Controllers** folder and double-click **cs**.
10. Locate the **Delete** method, and then click it. Press F9 to put a breakpoint on the **Delete** method.1. Press F5 to debug the application. After the browser opens, show that the page lists the destinations retrieved from the **Get** method of the DestinationsAPI controller.
11. Type **1** in the **Location id** box, and then click **delete**.
12. Show that the breakpoint in the **Delete** method is reached.
13. Press Shift+F5 to stop the debugger.

### Demonstration 2: Consuming Services Using HttpClient

#### Demonstration Steps

1. On the Start screen, click the **Visual Studio 2012** tile.
2. On the **File** menu, point to **Open** , and then click **Project/Solution.**
3. In the **File Name** text box, type **D:\Allfiles\Mod03\Democode\ConsumingFromHttpClient\Begin\HttpClientApplication\HttpClientApplication.sln** ,and then click **Open**.
4. In **Solution Explorer** , under the **Client** project, right-click **References** , and then click **Manage NuGet Packages**
5. Inside the **NuGet Manager** window, click **Online** on the left menu, and enter **AspNet.WebApi.Client** in the search box on the upper-right side.
6. In the search results list, locate the **Microsoft ASP.NET Web API 2.2 Client Libraries** ,and then click
7. If a **License Acceptance** dialog box appears, click **I Accept**.
8. After the installation is complete, click **Close** to close the **NuGet Manager** window.
9. In **Solution Explorer** , under the **Client** project, double-click **cs.**
10. Locate the **CallServer** method and code to send a GET request for the destinations resource and print the responses content as string. As soon as you are finished, the **CallServer** method should look as follows.

        static async Task CallServer()  
        {  
             var client = new HttpClient  
             {  
                  BaseAddress = new Uri(&quot;http://localhost:12534/&quot;)  
             };  
             HttpResponseMessage message = await client.GetAsync(&quot;api/Destinations&quot;);  
             var res = await message.Content.ReadAsStringAsync();  
             Console.WriteLine(res);  
        }  

11. Add code to deserialize the request content into a **List&lt;Destinations&gt;** by using the **ReadAsAsync&lt;T&gt;** method. As soon as you are finished, the **CallServer** method should look like this.

        static async Task CallServer()  
        {  
              var client = new HttpClient  
              {  
                     BaseAddress = new Uri(&quot;http://localhost:12534/&quot;)  
              };  
              
              HttpResponseMessage message = await client.GetAsync(&quot;api/Destinations&quot;);  
              var res = await message.Content.ReadAsStringAsync();  
              Console.WriteLine(res);  
              
              var destinations = await message.Content.ReadAsAsync&lt;List&lt;Destination&gt;&gt;();  
              Console.WriteLine(destinations.Count);  
        }

12. PressCtrl+S to save the changes.
13. In Solution Explorer, right-click the **Host** project, and then click **Set as StartUp Project**.
14. Press Ctrl+F5 to start the server application without debugging.
15. In Solution Explorer, under **HttpClientApplication.Client** project, double-click **cs**.
16. In the **Program** class, locate the **client** variable definition at the beginning of the **CallServer** method, and then click it. To put a breakpoint in that line, press **F9**.
17. In Solution Explorer, right-click the **Client** project, point to **Debug** , and then click **Start new instance** , and wait for a program to hit your breakpoint.
18. To execute the **CallServer** method line-by-line, press **F10**.
