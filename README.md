# ARDC.BizCard

Este repositório é um código legado de um projeto pessoal meu para gerar um app para criar e compartilhar cartões de visita em um padrão comum.

## Testes

### Unitários

`dotnet test` gg, wp

### UI Test

#### Configurando o ambiente

Garanta que o seu Computador possua as seguintes variáveis de ambiente devidamente configuradas:

+ `ANDROID_HOME`: `C:\Program Files (x86)\Android\android-sdk`
+ `JAVA_HOME`: `C:\Program Files\Android\Jdk\microsoft_dist_openjdk_1.8.0.25\bin`

Garanta também que o path da `ANDROID_HOME` possua **apenas uma pasta de** `platform-tools`. Caso você possua uma pasta como `platform-toolsold1234` delete-a!

#### Doctos de Referências

+ https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/openjdk
+ https://stackoverflow.com/questions/52254881/cannot-run-xamarin-ui-test-on-xamarin-forms-error-system-exception
+ https://bitbar.com/blog/setting-up-xamarin-uitest-framework-for-mobile-app-testing/
+ https://github.com/microsoft/appcenter-Xamarin.UITest-Demo/blob/master/UITestDemo.UITest/AppInitializer.cs#L13
+ https://www.c-sharpcorner.com/article/xamarin-ui-test/
+ https://forums.xamarin.com/discussion/27438/xamarin-ui-test-android-timed-out

## Git Flow

Use a colinha disponível em [GitFlow Cheatseet](https://danielkummer.github.io/git-flow-cheatsheet/) para entender o padrão de git deste repositório.