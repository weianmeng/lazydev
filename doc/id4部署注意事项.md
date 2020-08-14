### id4部署 

* 必须设定 IssuerUri  使.well-known/openid-configuration 地址一致

* nginx 代理 nginx不要配置跨越。跨越问由id4程序配置处理, 否则会出现多次跨越异常问题

* nginx 带里必须配置  详细参见https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-3.1

  ​	

  ```
          proxy_set_header   Host $host;
          proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
          proxy_set_header   X-Forwarded-Proto $scheme;
          
  ```

* 应用程序必须添加 接收原始请求协议

  ```c#
          var forwardOptions = new ForwardedHeadersOptions
          {
              ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
          	RequireHeaderSymmetry = false
          };
          forwardOptions.KnownNetworks.Clear();
          forwardOptions.KnownProxies.Clear();
          app.UseForwardedHeaders(forwardOptions);
  ```

* 自定义cliams  需要定义IProfileService 接口

  ```C#
      public class CustomProfileService: IProfileService
      {
          protected readonly ILogger Logger;
  
          public CustomProfileService(ILogger<CustomProfileService> logger)
          {
              Logger = logger;
          }
  
          public Task GetProfileDataAsync(ProfileDataRequestContext context)
          {
              context.LogProfileRequest(Logger);
              //添加自定义claims 进入token
              context.IssuedClaims.AddRange(context.Subject.Claims);
              context.LogIssuedClaims(Logger);
              return Task.CompletedTask;
          }
  
          public Task IsActiveAsync(IsActiveContext context)
          {
              Logger.LogDebug("IsActive called from: {caller}", (object)context.Caller);
              context.IsActive = true;
              return Task.CompletedTask;
          }
      }
  
  
  	//登录
      var identityServerUser = new IdentityServerUser(user.SubjectId)
      {
          DisplayName = user.Username
      };
  
      foreach (var claim in user.Claims)
      {
          identityServerUser.AdditionalClaims.Add(claim);
      }
      await HttpContext.SignInAsync(identityServerUser, props);
  
  
  
  
  
  ```

  

