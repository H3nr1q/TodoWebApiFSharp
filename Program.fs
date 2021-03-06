module Todos.Program

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Todos.Http
open System.Collections


let routes =
  choose [
    TodoHttp.handlers ]

let configureApp (app : IApplicationBuilder) =
  app.UseGiraffe routes

let configureServices (services : IServiceCollection) =
  let inMemory = Hashtable()
  services.AddSingleton<TodoFind>(TodoInMemory.find inMemory) |> ignore
  services.AddSingleton<TodoSave>(TodoInMemory.save inMemory) |> ignore  

[<EntryPoint>]
let main _ =
  WebHostBuilder()
    .UseKestrel()
    .Configure(Action<IApplicationBuilder> configureApp)
    .ConfigureServices(configureServices)
    .Build()
    .Run()
  0

  



