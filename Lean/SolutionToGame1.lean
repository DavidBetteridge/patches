-- lake build && lake env lean SolutionToGame1.lean
import Patches

open Patches

def board0 : Board :=
  { width := 6, height := 6, pieces := [
    { number := 1, shape := .wideRectangle, targetSize := some 2, rect := { left := 0, top := 0, right := 0, bottom := 0 } },
    { number := 2, shape := .any, targetSize := none, rect := { left := 5, top := 0, right := 5, bottom := 0 } },
    { number := 3, shape := .tallRectangle, targetSize := some 2, rect := { left := 1, top := 1, right := 1, bottom := 1 } },
    { number := 4, shape := .square, targetSize := none, rect := { left := 4, top := 1, right := 4, bottom := 1 } },
    { number := 5, shape := .tallRectangle, targetSize := some 2, rect := { left := 2, top := 2, right := 2, bottom := 2 } },
    { number := 6, shape := .tallRectangle, targetSize := some 2, rect := { left := 3, top := 2, right := 3, bottom := 2 } },
    { number := 7, shape := .tallRectangle, targetSize := some 3, rect := { left := 2, top := 3, right := 2, bottom := 3 } },
    { number := 8, shape := .tallRectangle, targetSize := some 3, rect := { left := 3, top := 3, right := 3, bottom := 3 } },
    { number := 9, shape := .tallRectangle, targetSize := none, rect := { left := 1, top := 4, right := 1, bottom := 4 } },
    { number := 10, shape := .tallRectangle, targetSize := some 3, rect := { left := 4, top := 4, right := 4, bottom := 4 } },
    { number := 11, shape := .any, targetSize := none, rect := { left := 0, top := 5, right := 0, bottom := 5 } },
    { number := 12, shape := .tallRectangle, targetSize := some 3, rect := { left := 5, top := 5, right := 5, bottom := 5 } },
  ] }

theorem initialBoardIsValid : initialBoardValid board0 = true := by decide

theorem move1IsValid : Rule2Valid board0 1 1 0 = true := by decide
def board1 : Board := applyMove board0 1 1 0

theorem move2IsValid : Rule2Valid board1 5 2 1 = true := by decide
def board2 : Board := applyMove board1 5 2 1

theorem move3IsValid : Rule2Valid board2 6 3 1 = true := by decide
def board3 : Board := applyMove board2 6 3 1

theorem move4IsValid : Rule2Valid board3 7 2 5 = true := by decide
def board4 : Board := applyMove board3 7 2 5

theorem move5IsValid : Rule2Valid board4 8 3 5 = true := by decide
def board5 : Board := applyMove board4 8 3 5

theorem move6IsValid : Rule2Valid board5 10 4 3 = true := by decide
def board6 : Board := applyMove board5 10 4 3

theorem move7IsValid : Rule2Valid board6 12 5 3 = true := by decide
def board7 : Board := applyMove board6 12 5 3

theorem move8IsValid : Rule1Valid board7 11 0 1 = true := by decide
def board8 : Board := applyMove board7 11 0 1

theorem move9IsValid : Rule1Valid board8 9 1 3 = true := by decide
def board9 : Board := applyMove board8 9 1 3

theorem move10IsValid : Rule1Valid board9 2 2 0 = true := by decide
def board10 : Board := applyMove board9 2 2 0

theorem move11IsValid : Rule2Valid board10 3 1 2 = true := by decide
def board11 : Board := applyMove board10 3 1 2

theorem move12IsValid : Rule2Valid board11 4 5 2 = true := by decide
def board12 : Board := applyMove board11 4 5 2

theorem move13IsValid : Rule1Valid board12 9 1 5 = true := by decide
def board13 : Board := applyMove board12 9 1 5

theorem move14IsValid : Rule1Valid board13 10 4 5 = true := by decide
def board14 : Board := applyMove board13 10 4 5

theorem solutionIsValid : solutionValid board14 = true := by decide
