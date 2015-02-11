README for CES.CoreApi.Logging

You might need to add the following sections to the web.config:

1.	Under <configuration><configSections> section add:

<section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net" /> 

2.	Under <configuration> section add:

<log4net>
    <root>
      <!-- Levels: OFF, DEBUG, INFO, NOTICE, WARN, ERROR, FATAL-->
      <level value="INFO" />
      <!--
				Log level priority in descending order:
      
				FATAL = 1  show  log -> FATAL 
				ERROR = 2  show  log -> FATAL ERROR 
				WARN =  3  show  log -> FATAL ERROR WARN 
				NOTICE = 4 show  log -> FATAL ERROR WARN NOTICE 
				INFO =  5  show  log -> FATAL ERROR WARN NOTICE INFO 
				DEBUG = 6  show  log -> FATAL ERROR WARN NOTICE INFO DEBUG
			-->
      <!--To write application database performance messages to file -->
      <appender-ref ref="DatabasePerformanceLogFileAppender" />
      <!--To write application database performance messages to grey log -->
      <appender-ref ref="DatabasePerformanceLogUdpAppender" />
      <!--To write application database performance messages to grey log -->
      <appender-ref ref="MethodPerformanceLogUdpAppender" />
      <appender-ref ref="MethodPerformanceLogFileAppender" />
    </root>
    <appender name="DatabasePerformanceLogFileAppender" type="log4net.Appender.RollingFileAppender">
      <file type="log4net.Util.PatternString" value="C:\logs\FxOnline_DBPerformance_%date{yyyy-MM-dd}.txt" />
      <lockingModel type="log4net.Appender.FileAppender+InterProcessLock" />
      <layout type="log4net.Layout.PatternLayout">
        <param name="ConversionPattern" value="%m%n" />
      </layout>
      <filter type="log4net.Filter.LevelMatchFilter">
        <levelToMatch value="NOTICE" />
      </filter>
      <filter type="log4net.Filter.DenyAllFilter" />
    </appender>
    <appender name="DatabasePerformanceLogUdpAppender" type="gelf4net.Appender.GelfUdpAppender, Gelf4net">
      <remoteAddress value="10.30.8.6" />
      <remotePort value="12200" />
      <layout type="gelf4net.Layout.GelfLayout, Gelf4net">
        <param name="AdditionalFields" value="app:FxOnline,logtype:DatabasePerformanceLog,thread:%thread,level:%level,logger:%logger" />
        <param name="ConversionPattern" value="%message" />
      </layout>
      <filter type="log4net.Filter.LevelMatchFilter">
        <levelToMatch value="NOTICE" />
      </filter>
      <filter type="log4net.Filter.DenyAllFilter" />
    </appender>

    <appender name="MethodPerformanceLogUdpAppender" type="gelf4net.Appender.GelfUdpAppender, Gelf4net">
      <remoteAddress value="10.30.8.6" />
      <remotePort value="12200" />
      <layout type="gelf4net.Layout.GelfLayout, Gelf4net">
        <param name="AdditionalFields" value="app:FxOnline,logtype:MethodPerformanceLog,thread:%thread,level:%level,logger:%logger" />
        <param name="ConversionPattern" value="%message" />
      </layout>
      <filter type="log4net.Filter.LevelMatchFilter">
        <levelToMatch value="INFO" />
      </filter>
      <filter type="log4net.Filter.DenyAllFilter" />
    </appender>
    
    <appender name="MethodPerformanceLogFileAppender" type="log4net.Appender.RollingFileAppender">
      <file type="log4net.Util.PatternString" value="C:\logs\FxOnline_MethodPerformance_%date{yyyy-MM-dd}.txt" />
      <lockingModel type="log4net.Appender.FileAppender+InterProcessLock" />
      <layout type="log4net.Layout.PatternLayout">
        <param name="ConversionPattern" value="%m%n" />
      </layout>
      <filter type="log4net.Filter.LevelMatchFilter">
        <levelToMatch value="INFO" />
      </filter>
      <filter type="log4net.Filter.DenyAllFilter" />
    </appender>
  </log4net>

3.	Under <appSettings> section add:

<add key="dbPerformanceLogThreshold" value="0" />
<add key="dbPerformanceLogEnabled" value="true" />
<add key="performanceLogThreshold" value="0" />
<add key="performanceLogEnabled" value="true" />
<add key="performanceLogCacheRegion" value="PerfMonDev" />
<add key="CacheLifetime" value="0.00:10:00" />

4.	Under <system.webServer><modules> section add:

<add name="FxHubPerformanceLogApplicationContextModule" type="FxCore.Authorization.Url.PerformanceLogApplicationContextModule" />

5.	Under <runtime><assemblyBinding section make sure next DLLs exist:

<dependentAssembly>
	<assemblyIdentity name="log4net" publicKeyToken="669e0ddf0bb1aa2a" culture="neutral" />
	<bindingRedirect oldVersion="0.0.0.0-1.2.13.0" newVersion="1.2.13.0" />
</dependentAssembly>
<dependentAssembly>
	<assemblyIdentity name="Microsoft.Practices.EnterpriseLibrary.Caching" publicKeyToken="31bf3856ad364e35" culture="neutral" />
	<bindingRedirect oldVersion="0.0.0.0-5.0.505.0" newVersion="5.0.505.0" />
</dependentAssembly>
<dependentAssembly>
	<assemblyIdentity name="Microsoft.Practices.EnterpriseLibrary.Common" publicKeyToken="31bf3856ad364e35" culture="neutral" />
	<bindingRedirect oldVersion="0.0.0.0-5.0.505.0" newVersion="5.0.505.0" />
</dependentAssembly>
<dependentAssembly>
	<assemblyIdentity name="Microsoft.Practices.Unity" publicKeyToken="31bf3856ad364e35" culture="neutral" />
	<bindingRedirect oldVersion="0.0.0.0-2.1.505.0" newVersion="2.1.505.0" />
</dependentAssembly>
<dependentAssembly>
	<assemblyIdentity name="Microsoft.Practices.EnterpriseLibrary.Validation" publicKeyToken="31bf3856ad364e35" culture="neutral" />
	<bindingRedirect oldVersion="0.0.0.0-5.0.505.0" newVersion="5.0.505.0" />
</dependentAssembly>
<dependentAssembly>
	<assemblyIdentity name="SimpleInjector" publicKeyToken="984cb50dea722e99" culture="neutral" />
	<bindingRedirect oldVersion="0.0.0.0-2.7.1.0" newVersion="2.7.1.0" />
</dependentAssembly>


6.	Under <system.serviceModel><extensions><behaviorExtensions> update DLL version.
It should be:

<add name="validation" type="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF.ValidationElement, Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF, Version=5.0.505.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
