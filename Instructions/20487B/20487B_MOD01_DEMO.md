# Module 2: Designing ASP.NET MVC 4 Web Applications

# Lab: Designing ASP.NET MVC 4 Web Applications

### Exercise 1: Planning Model Classes

#### Task 1: Examine the initial investigation.

1. In the result pane of the Hyper-V Manager console, in the **Name** list of the **Virtual Machines** area, right-click **20486B-SEA-DEV11** , and then click **Connect**.
2. To log on to **20486B-SEA-DEV11** , click the **Ctrl+Alt+Delete** button.
3. In the **User name** box, type **Admin** , in the **Password** box, type **Pa$$w0rd** , and then click the **Forward** button.
4. On the Windows 8 Start screen, click **Desktop**.
5. On the taskbar, click the **File Explorer** icon.
6. In the **Libraries** window, navigate to **Allfiles (D):\Labfiles\Mod02**, and then double-click **InitialInvestigation.docx**.
7. On the **View** tab of the **InitialInvestigation.docx - Microsoft Word** window, in the **Show** group, ensure that the **Navigation Pane** check box is selected.
8. In the Navigationpane, click **Introduction** to view the contents of the Introduction section.
9. In the Navigationpane, click **General Description of the Photo Sharing Application** to view the contents.
10. In the Navigationpane, click **Use Cases** to view the contents.
11. In the Navigationpane, under Use Cases, view all the Use Cases in the document.
12. In the InitialInvestigation.docx - Microsoft Word window, review Figure 1: Use Case Summary.
13. In the **InitialInvestigation.docx -**** Microsoft Word **window, click the** Close** button.

#### Task 2: Plan the photo model class.

1. On the taskbar, click the **File Explorer** icon.

2. In the **D:\Labfiles\Mod02** window, double-click **DetailedPlanningDocument.docx**.
3. In the **DetailedPlanningDocument.docx - Microsoft Word** window, locate the MVC Model section.
4. In the MVC Model section, locate the **Table 1: MVC Model** table.
5. In the first row of the **Model Class** column, type _&lt;Model class name1&gt;._
6. In the first row of the **Description** column, type _&lt;Description of the model class&gt;_.
7. In the **Table 1: MVC Model** table, in the first row of the **Properties** column, type _&lt;Property1&gt;_.
8. Add other properties to the model class.
9. In the **Table 1: MVC Model** table, in the first row of the **Data Types** column, type _&lt;Data type1&gt;_.
10. Add other data types corresponding to the photo properties.
11. In the **Model Class** column of the **Table 1: MVC Model** table, select all the rows adjacent to the properties you have added, right-click, and then click **Merge Cells**.
12. In the **Description** column of the **Table 1: MVC Model** table, select all the rows adjacent to the properties you have added, right-click, and then click **Merge Cells**.
13. On the Quick Access Toolbar, click **Save**.
14. On the taskbar, move the pointer to the lower-left corner, and then click the **Start** icon.
15. On the Windows 8 Start screen, click **Microsoft Visio 2010**.
16. On the **File** tab of the **Microsoft Visio** window, click the **New** tab, and then, in the Template Categories section, click **Software and Database**.
17. In the Choose a Template section, click **UML Model Diagram** , click **Metric Units** , and then, in the result pane, click **Create**. If prompted to add a new template, click **Yes**.
18. In the Shapes pane of the **Drawing1 - Microsoft Visio** window, ensure **UML Static Structure (Metric)** is selected.
19. In the **UML Static Structure** stencil, drag the **Class** shape onto the drawing page.
20. On the drawing page, right-click the **Class** shape, and then click **Properties**.
21. In the **Name** box of the **UML Class Properties** dialog box, type _&lt;Model name1&gt;_.
22. In the Categories pane of the **UML Class Properties** dialog box, click **Attributes**.
23. In the **Attributes** table, in the first row of the **Attribute** column, type _&lt;Property1&gt;_.
24. In the first row of the **Type** column, click _&lt;Data type1&gt;_.
25. Add the attributes for all the properties you added, and then click **OK**.
26. On the Quick Access Toolbar of the **Drawing1 - Microsoft Visio** window, click **Save**.
27. In the **Save As** dialog box, navigate to **Allfiles (D):\Labfiles\Mod02**, in the **File name** box, type **PhotoSharingLDM** , and then click **Save**.

#### Task 3: Plan the comment model class.

1. On the taskbar, click the **Microsoft Word** icon.

2. In the **DetailedPlanningDocument.docx - Microsoft Word** window, locate the MVC Model section.
3. In the **DetailedPlanning Document.docx - Microsoft Word** window, locate the **Table 1: MVC Model** table.
4. In the second row of the **Model Class** column, type _&lt;Model name2&gt;_.
5. In the second row of the **Description** column, type _&lt;Description of the model&gt;_.
6. In the **Properties** column of the **Table 1: MVC Model** table, add the properties corresponding to the model class you created.
7. In the **Data**** Types **column of the** Table 1: MVC Model** table, add data types corresponding to the comment properties.
8. In the **Model Class** column of the **Table 1: MVC Model** table, select all the rows adjacent to the properties you added, right-click, and then click **Merge Cells**.

    >**Note:** The rows in the **Model Class** column should only be merged if you have created more than one property.

9. In the **Description** column, select all the rows adjacent to the properties you added, right-click, and then click **Merge Cells**.

    >**Note:** The rows in the **Description** column should only be merged if you have created more than one property.

10. On the Quick Access Toolbar, click **Save**.
11. On the taskbar, click the **PhotoSharingLDM.vsd - Microsoft Visio 2010** icon.
12. In the Shapes pane of the **PhotoSharingLDM.vsd - Microsoft Visio** window, ensure that the **UML Static Structure (Metric)** stencil is selected.
13. In the **UML Static Structure** stencil, drag the **Class** shape onto the drawing page.
14. On the drawing page, right-click the **Class** shape, and then click **Properties**.
15. In the **Name** box of the **UML Class Properties** dialog box, type _&lt;Model name2&gt;._
16. In the Categories pane of the **UML Class Properties** dialog box, click **Attributes**.
17. In the **Attributes** table, in the first row of the **Attribute** column, type the name of the property you added.
18. In the first row of the **Type** column, select the corresponding data type.
19. Add the attributes for all the properties you added, and then click **OK**.
20. On the **Home** tab of the **PhotoSharingLDM.vsd - Microsoft Visio** window, in the **Tools** group, click **Connector**.
21. On the drawing page, move the mouse pointer over one of the model classes, click the blue cross, and then drag the connector to the second model class.
22. On the drawing page, right-click the connector, and then click **Shape Display Options**.
23. In the End Options section of the **UML Shape Display Options** dialog box, clear the **First end name** and the **Second end name** check boxes, and then click **OK**.
24. On the drawing page of **PhotoSharingLDM.vsd - Microsoft Visio** window, right-click the connector, and then click **Properties**.
25. In the Categories pane of the **UML Association Properties** dialog box, ensure that Association is selected.
26. In the **Association Ends** table, for each end of the connector, select an appropriate value for the **Multiplicity** column, and then click **OK**.

       >**Note:** The values you choose should be determined by the number of photos that can be associated with a comment and the number of comments that can be associated with a photo.

27. On the Quick Access Toolbar of the **PhotoSharingLDM.vsd - Microsoft Visio** window, click **Save**.

    >**Results** : After completing this exercise, you will be able to create proposals for a model, and configure the properties and data types of the model classes.

### Exercise 2: Planning Controllers

#### Task 1: Plan the photo controller.

1. On the taskbar, click the **Microsoft Word** icon.

2. In the **DetailedPlanningDocument.docx - Microsoft Word** window, locate the MVC Controllers section.
3. In the **DetailedPlanning Document.docx - Microsoft Word** window, locate the **Table 2: MVC Controllers** table.
4. In the first row of the **Controller** column, type _&lt;Controller1&gt;_.
5. In the first row of the **Action** column, type _&lt;Action1&gt;_.
6. Add other required actions for the controller for photos.
7. In the first row of the **Description** column, type _&lt;Description1&gt;._
8. Add other descriptions to the subsequent rows of the **Action** column.

    >**Note:** Describe when each action is run, what it does, and any views it returns.

9. In the **Controller** column of the **Table 2: MVC Controllers** table, select all the rows adjacent to the **Action** column, right-click, and then click **Merge Cells**.
10. On the Quick Access Toolbar, click **Save**.

#### Task 2: Plan the comment controller.

1. In the **Table 2: MVC Controllers** table, in the second row of the **Controller** column, type _&lt;Controller2&gt;_.

2. In the **Action** column, corresponding to _&lt;Controller2&gt;_, type_&lt;Action1&gt;_.
3. Add other required actions in the subsequent rows of the **Action** column.
4. In the **Description** column, type the description of the corresponding actions.
5. In the **Controller** column of the **Table 2: MVC Controllers** table, select all the adjacent rows to the actions you planned, right-click, and then click **Merge Cells**.
6. On the Quick Access Toolbar, click **Save**.

    >**Results** : After completing this exercise, you will be able to create proposals for controllers and configure their properties and data types.

### Exercise 3: Planning Views

#### Task 1: Plan the single photo view.

1. In the **DetailedPlanningDocument.docx - Microsoft Word** window, locate the MVC Views section, and then locate the **Table 3:**** MVC Views** table.

2. In the first row of the **Controller** column, type _&lt;Controller1&gt;_.
3. Add other required controllers to the **Table 3:**** MVC Views**.
4. In the first row of the **View** column, type _&lt;View1&gt;._
5. In the subsequent rows of the **View** column, add other required views corresponding to the controllers.
6. In the **Description** column, type the description of the corresponding views.
7. In the **Controller** column of the **Table 3:**** MVC Views **table, select all the rows adjacent to the actions you planned for _&lt;Controller1&gt;_, right-click, and then click** Merge Cells**.
8. In the **Controller** column, merge the rows corresponding to the other controllers.
9. On the Quick Access Toolbar, click **Save**.
10. On the taskbar, click the **PhotoSharingLDM.vsd - Microsoft Visio** icon.
11. On the **File** tab of the **PhotoSharingLDM.vsd - Microsoft Visio** window, click the **New** tab.
12. In the Template Categoriessection of the **New** tab, click **Software and Database**.
13. In the Choose a Template section, click **Wireframe Diagram** , and then, in the result pane, click **Create**.
14. In the Shapes pane of the **Drawing1 - Microsoft Visio** window, ensure **Dialogs (Metric)** is selected.
15. In the **Dialogs**** (Metric) **stencil, drag the** Application Form** shape to the drawing page.
16. On the drawing page, resize the new shape to fill most of the page.
17. In the **Application Form** shape, double-click **Application Title** , and then type the title you want to be displayed.
18. In the **Dialogs**** (Metric) **stencil, drag the** Panel **shape to the** Application Form** shape, and drop it under the title.
19. Resize the panel to fill the width of the **Application Form** shape.
20. Resize the panel to the same height as the **Application Form** shape.
21. In the **Shapes** pane, click **Controls (Metric)**.
22. In the **Controls**** (Metric) **stencil, drag a** Hyperlink** shape to the panel.
23. In the **Hyperlinks** dialog box, click **Cancel**.
24. On the drawing page of the **Drawing2 - Microsoft Visio** window, double-click **Hyperlink** , and then type **Home**.
25. Add links for **Gallery** , **Add a Photo** , and **Slideshow** on the panel.
26. In the **Shapes** pane, click **Dialogs (Metric)**.
27. In the **Dialogs (Metric)** stencil, drag a **Panel** shape to the **Application Form** shape, and drop the panel beneath the menu.
28. Resize the panel to display a large photo.
29. In the **Shapes** pane, click **Web and Media Icons (Metric)**.
30. In the **Web and Media Icons**** (Metric) **stencil, drag a** Photos** shape to the panel you created.

    >**Note:** Add other elements to the wireframe diagram that you feel are necessary based on your reading of the **InitialInvestigation** document. Steps will vary, depending on the elements you feel are necessary. ** **

31. On the Quick Access Toolbar of the **Drawing2 - Microsoft Visio** window, click **Save**.
32. In the **Save As** dialog box, navigate to **Allfiles (D):\Labfiles\Mod02**, in the **File name** box, type **SinglePhotoWireframe** , and then click **Save**.

#### Task 2: Plan the gallery view.

1. On the **File** tab of the **SinglePhotoWireframe.vsd - Microsoft Visio** window, click the **New** tab.

2. In the Template Categoriessection of the **New** tab, click **Software and Database**.
3. In the Choose a Template section, click **Wireframe Diagram** , and then, in the result pane, click **Create**.
4. In the **Shapes** pane of the **Drawing3 - Microsoft Visio** window, ensure **Dialogs (Metric)** is selected.
5. In the **Dialogs**** (Metric) **stencil, drag an** Application Form** shape to the drawing page.
6. On the drawing page, resize the new shape to fill most of the page.
7. In the **Application Form** shape, double-click **Application Title** , and type the title you want to be displayed.
8. In the **Dialogs**** (Metric) **stencil, drag the** Panel **shape to the** Application Form** shape, and drop it under the title.
9. Resize the panel to fill the width of the **Application Form** shape.
10. Resize the panel to be approximately the same height as the title.
11. In the **Shapes** pane, click **Controls (Metric)**.
12. In the **Controls**** (Metric) **stencil drag a** Hyperlink** shape to the panel.
13. In the **Hyperlinks** dialog box, click **Cancel**.
14. On the drawing page of the **Drawing3 - Microsoft Visio** window, double-click **Hyperlink** , and then type **Home**.
15. On the panel, add **Hyperlinks** with the following labels: **Gallery** , **Add a Photo** , and **Slideshow**.
16. In the **Shapes** pane, click **Dialogs (Metric)**.
17. In the **Dialogs**** (Metric) **stencil, drag a** Panel **shape to the** Application Form** shape, and drop the panel beneath the menu.
18. Resize the panel to display a thumbnail photo.
19. In the **Shapes** pane, click **Web and Media Icons (Metric)**.
20. In the **Web and Media Icons (Metric)** stencil, drag a **Photos** shape to the panel you created.

    >**Note:** You can add multiple panels to display many thumbnails in the Gallery view.

     >**Note:** Add other elements to the wireframe diagram that you feel are necessary based on your reading of the **InitialInvestigation** document.

    >Steps will vary, depending on the elements you feel are necessary.

21. On the Quick Access Toolbar, click **Save**.
22. In the **Save As** dialog box, navigate to **Allfiles (D):\Labfiles\Mod02**, in the **File name** box, type **PhotoGalleryWireframe** , and then click **Save**.

    >**Results** : After completing this exercise, you will create proposals for views and their layouts.

### Exercise 4: Architecting an MVC Web Application

#### Task 1: Hosting options.

1. On the taskbar, click the **DetailedPlanningDocument(docx) - Microsoft Word** icon.

2. In the **DetailedPlanningDocument(docx) - Microsoft Word** window, locate the Hosting Recommedations section, and then locate the Web Server section.
3. Under Web Server, type the recommended host and web server configuration **.**

#### Task 2: Choose a data store.

1. In the Database sectionof the **DetailedPlanningDocument(docx) - Microsoft Word** window, type the recommended database and server configuration.

2. On the Quick Access Toolbar, click **Save**.

    >**Results** : After completing this exercise, you will be able to create proposals for hosting arrangements.