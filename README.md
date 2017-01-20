# Power BI TagHelper
The aim of this project to create a simple and useful TagHelper for embedding Power BI report(s) into your ASP.NET Core application. This will let you expose your Power BI report(s) from:

## PowerBI Portal (PowerBI Publish to Web)

This option will let you share and view your Power BI report(s) without any authentication, so your report(s) will be public accessible to everyone on the internet.

**Usage:**

```html
<power-bi reportId="Your ReportId" />
```

## Microsoft Azure Portal (Power BI Embedded)

This option will let you share and view your Power BI report(s) with authentication, so Access Key and Workspace information are required here.

**Usage:**

```html
<power-bi workspaceCollectionName="Your WorkspaceCollectionName"
          workspaceId="Your WoekspaceId"
          accessKey="Your AccessKey"
          reportId="Your ReportId" />
```