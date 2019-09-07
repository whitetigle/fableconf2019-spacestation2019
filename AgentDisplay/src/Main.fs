module Main
open Fable.Core
open Fable.Core.JsInterop
open Elmish
open Thoth.Json

module Types = 

  open Types

  type ActivePage =
    | Welcome of Page.Welcome.Types.Model    
  
  type Msg = 
    | WelcomeMsg of Page.Welcome.Types.Msg  
    | OnDisplaySuccess
  
  type Model = 
    {
      ActivePage:ActivePage option
      CurrentRoute:Route option
      IsLoading:bool
    }

let displayError errorMessage (model:Types.Model)=
    printfn "%s" errorMessage
    { model with IsLoading=false}, Cmd.none

let displaySuccess (model:Types.Model)=
    { model with IsLoading=false}, Cmd.none

module State = 

  open Types
  open Elmish
  open Elmish.Navigation
  open Elmish.UrlParser

  module Update =

    let load model =
      {model with IsLoading=true}

    let loadFinished model =
      {model with IsLoading=false}

    let activePage activePage route model =
      {
        model with
          IsLoading=false
          ActivePage=Some activePage
      },Router.modifyUrl route

    let Page activePage route model =
      {
        model with
          IsLoading=false
          ActivePage=Some activePage
      },Router.modifyUrl route

    let routeMessage activePage mapper command model =
      { model with ActivePage=Some activePage}, Cmd.map mapper command

  let rec setRoute (optRoute: Option<Route>) model =
    { model with CurrentRoute = optRoute }, Cmd.none

  let init location : Model * Cmd<Msg> = 
    
    let (model, cmd) =
      setRoute location
        {
          ActivePage=Some ( ActivePage.Welcome Page.Welcome.Types.initModel)
          CurrentRoute=Some Route.Welcome
          IsLoading=false
        }
    
    model, cmd

  let nextPage route model =
    setRoute (Some route) model
        
  let update msg model = 
      match model.ActivePage, msg with
      // | _, MyCommandIndependantOfAnyPage  ->  placesholder for other commands
      | Some page, msg ->
        match page,msg with      

        | Welcome md, WelcomeMsg msg -> 
          match msg with 
          | _ -> 
              let updated, cmd = Page.Welcome.State.update msg md
              model
                |> Update.routeMessage (ActivePage.Welcome updated) WelcomeMsg cmd

        | _ -> 
          model, Cmd.none

      | _ -> 
        model, Cmd.none

module View = 

  open Types
  open Fulma
  open Fulma.Extensions
  open Fable.Core.JsInterop
  open Fable.React
  open Fable.React.Props
  
  let root (model:Model) dispatch = 
          
    let publicView (model:Model)  view =
      Hero.hero
        [ ]
        [
          Hero.body [] [view]          
        ]
        
    match model.ActivePage with 
    | Some page -> 
      match page with 
      | (ActivePage.Welcome md) -> 
          Page.Welcome.View.root md (WelcomeMsg >> dispatch)
            |> publicView model 

    | None -> 

      // should add some loading screen there ;)
      div [] [str ""]
