import Patches

open Patches

def board0 : Board :=
  { width := 6, height := 6, pieces := [
    { number := 1, shape := .tallRectangle, targetSize := some 2, rect := { left := 1, top := 0, right := 1, bottom := 0 } },
    { number := 2, shape := .any, targetSize := some 2, rect := { left := 0, top := 1, right := 0, bottom := 1 } },
    { number := 3, shape := .square, targetSize := some 4, rect := { left := 2, top := 1, right := 2, bottom := 1 } },
    { number := 4, shape := .any, targetSize := some 4, rect := { left := 4, top := 1, right := 4, bottom := 1 } },
    { number := 5, shape := .any, targetSize := some 6, rect := { left := 1, top := 4, right := 1, bottom := 4 } },
    { number := 6, shape := .tallRectangle, targetSize := some 6, rect := { left := 3, top := 4, right := 3, bottom := 4 } },
    { number := 7, shape := .any, targetSize := some 6, rect := { left := 5, top := 4, right := 5, bottom := 4 } },
    { number := 8, shape := .wideRectangle, targetSize := some 6, rect := { left := 4, top := 5, right := 4, bottom := 5 } },
  ] }

theorem initialBoardIsValid : initialBoardValid board0 = true := by decide

theorem move1IsValid : Rule2Valid board0 1 1 1 = true := by decide
def board1 : Board := applyMove board0 1 1 1

theorem move2IsValid : Rule2Valid board1 6 3 3 = true := by decide
def board2 : Board := applyMove board1 6 3 3

theorem move3IsValid : Rule2Valid board2 7 5 2 = true := by decide
def board3 : Board := applyMove board2 7 5 2

theorem move4IsValid : Rule2Valid board3 8 0 5 = true := by decide
def board4 : Board := applyMove board3 8 0 5

theorem move5IsValid : Rule2Valid board4 8 5 5 = true := by decide
def board5 : Board := applyMove board4 8 5 5

theorem move6IsValid : Rule1Valid board5 2 0 0 = true := by decide
def board6 : Board := applyMove board5 2 0 0

theorem move7IsValid : Rule1Valid board6 5 0 3 = true := by decide
def board7 : Board := applyMove board6 5 0 3

theorem move8IsValid : Rule1Valid board7 3 2 0 = true := by decide
def board8 : Board := applyMove board7 3 2 0

theorem move9IsValid : Rule1Valid board8 4 4 0 = true := by decide
def board9 : Board := applyMove board8 4 4 0

theorem move10IsValid : Rule2Valid board9 3 3 1 = true := by decide
def board10 : Board := applyMove board9 3 3 1

theorem move11IsValid : Rule2Valid board10 6 3 2 = true := by decide
def board11 : Board := applyMove board10 6 3 2

theorem move12IsValid : Rule2Valid board11 7 4 2 = true := by decide
def board12 : Board := applyMove board11 7 4 2

theorem move13IsValid : Rule1Valid board12 5 0 2 = true := by decide
def board13 : Board := applyMove board12 5 0 2

theorem move14IsValid : Rule1Valid board13 6 2 2 = true := by decide
def board14 : Board := applyMove board13 6 2 2

theorem move15IsValid : Rule1Valid board14 4 5 1 = true := by decide
def board15 : Board := applyMove board14 4 5 1

theorem solutionIsValid : solutionValid board15 = true := by decide
