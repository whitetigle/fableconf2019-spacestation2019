namespace RiddleGarden

[<RequireQualifiedAccess>]
module Types = 

  open Fable.Core 
  open Thoth.Json

  let [<Literal>] COLS = 32
  let [<Literal>] ROWS = 18

  [<StringEnum>]
  type CellKind = 
    | Flower
    | Drop
    | Seed
    | Mountain
    | Volcano
    | Unknown

  [<StringEnum>]
  type Background = 
    | Water 
    | Rock
    | Ground

  [<StringEnum>]
  type Size = 
    | Small
    | Medium
    | Big
    | Bigger

  type Cell = 
    {
      X:int
      Y:int
      CellKind:CellKind 
      Background:Background 
      Size: Size
    }
    static member Encoder cell= 
      let symbol = 
        (string cell.CellKind).ToUpperInvariant()

      let background = 
        (string cell.Background).ToUpperInvariant()
        
      let size = 
        (string cell.Size).ToUpperInvariant()
        
      Encode.object [
        "x", Encode.int cell.X
        "y", Encode.int cell.Y
        "cellKind", Encode.string symbol
        "background", Encode.string background
        "size", Encode.string size
      ]
    static member Decoder = 
      let decodeSymbol = 
        Decode.string
            |> Decode.andThen
          (function
            | "FLOWER" -> Decode.succeed CellKind.Flower  
            | "DROP" -> Decode.succeed CellKind.Drop 
            | "SEED" -> Decode.succeed CellKind.Seed 
            | "MOUNTAIN" -> Decode.succeed CellKind.Mountain 
            | "VOLCANO" -> Decode.succeed CellKind.Volcano 
            | "UNKNOWN" -> Decode.succeed CellKind.Unknown 
            | invalid -> Decode.fail(sprintf "Failed to decode `%s` it's an invalid case for `Symbol`" invalid))
                

      let decodeBackground = 
        Decode.string
            |> Decode.andThen
                (function
                | "WATER" -> Decode.succeed Background.Water
                | "GROUND" -> Decode.succeed Background.Rock
                | "ROCK" -> Decode.succeed Background.Ground
                | invalid -> Decode.fail(sprintf "Failed to decode `%s` it's an invalid case for `Background`" invalid))

      let sizeDecoder = 
        Decode.string
            |> Decode.andThen
                (function
                | "SMALL" -> Decode.succeed Size.Small
                | "MEDIUM" -> Decode.succeed Size.Medium
                | "BIG" -> Decode.succeed Size.Big
                | "BIGGER" -> Decode.succeed Size.Bigger
                | invalid -> Decode.fail(sprintf "Failed to decode `%s` it's an invalid case for `Size`" invalid))

      Decode.object
        (fun get ->
            { 
              X = get.Required.Field "x" Decode.int
              Y = get.Required.Field "y" Decode.int
              CellKind = get.Required.Field "cellKind" decodeSymbol
              Background = get.Required.Field "background" decodeBackground
              Size = get.Required.Field "size" sizeDecoder
            }
        )

  type Tiles = Cell [] 
  type World = { Tiles: Tiles }
  type Test = { Test : string array }

  module Helper = 

    module private Utils = 
      [<RequireQualifiedAccess>]
      module Seq =
        let split n s =
            seq {
                let r = ResizeArray<_>()
                for x in s do
                    r.Add(x)
                    if r.Count = n then
                        yield r.ToArray()
                        r.Clear()
                if r.Count <> 0 then
                    yield r.ToArray()
            }

    module World = 
      
      open Fable.Core.JsInterop

      let encode (tiles:Tiles) =
        tiles 
          |> Array.map Cell.Encoder 
          |> Encode.array
          |> fun tiles -> Encode.object [ "tiles", tiles ] 

      let Decoder : Decoder<World> = 
        Decode.object
          (fun get -> 
            let tiles = 
              get.Required.Field "tiles" (Decode.array Cell.Decoder)
            
            { Tiles = tiles }
          )
