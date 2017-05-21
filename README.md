# Xamagram 

El objetivo en este ejemplo es modificar la aplicación Xamarin.Forms **Xamagram** para **añadir soporte a Azure Mobile Service**.

## Añadir paquete NuGet

El primer cambio a realizar es añadir a cada uno de los proyectos de la solución el paquete **NuGet** correspondiente a **Microsoft.Azure.Mobile.Client**.

![](Images/nuget.png)

## Inicializar la librería

Android e iOS necesitan inicializar la librería correspondiente al cliente Azure. Esta tarea debe realizarse en cada proyecto nativo específico de cada plataforma.

**Android**

Se debe realizar una llamada a **Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();** en el método OnCreate de la actividad principal.

    protected override void OnCreate(Bundle bundle)
    {
         base.OnCreate(bundle);
    
         Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init ();
    
         global::Xamarin.Forms.Forms.Init(this, bundle);
         LoadApplication(new App());
    }

**iOS**

En iOS se debe llamar a **Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();** en el método FinishedLaunching del delegado de la aplicación.

    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
         Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init ();
    
         global::Xamarin.Forms.Forms.Init();
         LoadApplication(new App());
    
         return base.FinishedLaunching(app, options);
    }

## Crear nuevo servicio para conectar con Azure