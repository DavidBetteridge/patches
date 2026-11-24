namespace Patches

inductive Shape where
  | any
  | square
  | tallRectangle
  | wideRectangle
  deriving DecidableEq, Repr

structure Rect where
  left : Nat
  top : Nat
  right : Nat
  bottom : Nat
  deriving DecidableEq, Repr

structure Piece where
  number : Nat
  shape : Shape
  targetSize : Option Nat
  rect : Rect
  deriving DecidableEq, Repr

structure Board where
  width : Nat
  height : Nat
  pieces : List Piece
  deriving Repr

def Rect.width (r : Rect) : Nat := r.right - r.left + 1

def Rect.height (r : Rect) : Nat := r.bottom - r.top + 1

def Rect.size (r : Rect) : Nat := r.width * r.height

def Rect.contains (r : Rect) (x y : Nat) : Bool :=
  decide (r.left ≤ x ∧ x ≤ r.right ∧ r.top ≤ y ∧ y ≤ r.bottom)

def Rect.overlaps (a b : Rect) : Bool :=
  decide (a.left ≤ b.right ∧ b.left ≤ a.right ∧ a.top ≤ b.bottom ∧ b.top ≤ a.bottom)

def Rect.within (r : Rect) (b : Board) : Bool :=
  decide (r.left ≤ r.right ∧ r.top ≤ r.bottom ∧ r.right < b.width ∧ r.bottom < b.height)

def Rect.growToInclude (r : Rect) (x y : Nat) : Rect :=
  { left := min r.left x
    top := min r.top y
    right := max r.right x
    bottom := max r.bottom y }

def shapeMatches (shape : Shape) (r : Rect) : Bool :=
  match shape with
  | .any => true
  | .square => r.width == r.height
  | .tallRectangle => r.width < r.height
  | .wideRectangle => r.width > r.height

def Board.cells (b : Board) : List (Nat × Nat) :=
  (List.range b.width).flatMap fun x =>
    (List.range b.height).map fun y => (x, y)

def allRects (b : Board) : List Rect :=
  (List.range b.width).flatMap fun left =>
    (List.range b.height).flatMap fun top =>
      (List.range b.width).flatMap fun right =>
        (List.range b.height).filterMap fun bottom =>
          if left ≤ right && top ≤ bottom then
            some { left := left, top := top, right := right, bottom := bottom }
          else
            none

def validPlacement (b : Board) (p : Piece) (r : Rect) : Bool :=
  r.within b &&
  decide (r.left ≤ p.rect.left ∧ p.rect.right ≤ r.right ∧
    r.top ≤ p.rect.top ∧ p.rect.bottom ≤ r.bottom) &&
  (p.targetSize == none || p.targetSize == some r.size) &&
  shapeMatches p.shape r &&
  b.pieces.all fun other =>
    other.number == p.number || !(r.overlaps other.rect)

def possibleGrowths (b : Board) (p : Piece) : List Rect :=
  (allRects b).filter fun r => r != p.rect && validPlacement b p r

def Board.cellEmpty (b : Board) (x y : Nat) : Bool :=
  b.pieces.all fun p => !(p.rect.contains x y)

def canGrowInto (b : Board) (p : Piece) (x y : Nat) : Bool :=
  (possibleGrowths b p).any fun r => r.contains x y

def Rule1Valid (b : Board) (pieceNumber x y : Nat) : Bool :=
  x < b.width && y < b.height && b.cellEmpty x y &&
  b.pieces.any (fun p => p.number == pieceNumber && canGrowInto b p x y) &&
  b.pieces.all (fun p => p.number == pieceNumber || !(canGrowInto b p x y))

def Rule2Valid (b : Board) (pieceNumber x y : Nat) : Bool :=
  x < b.width && y < b.height && b.cellEmpty x y &&
  match b.pieces.find? (fun p => p.number == pieceNumber) with
  | none => false
  | some p =>
    let growths := possibleGrowths b p
    !growths.isEmpty && growths.all (fun r => r.contains x y)

def applyMove (b : Board) (pieceNumber x y : Nat) : Board :=
  { b with pieces := b.pieces.map fun p =>
      if p.number == pieceNumber then
        { p with rect := p.rect.growToInclude x y }
      else
        p }

def initialBoardValid (b : Board) : Bool :=
  b.width > 0 && b.height > 0 &&
  b.pieces.all (fun p =>
    p.rect.left == p.rect.right && p.rect.top == p.rect.bottom && p.rect.within b) &&
  b.pieces.all (fun p =>
    (b.pieces.filter fun q => q.number == p.number).length == 1) &&
  b.pieces.all (fun p =>
    b.pieces.all (fun q => p.number == q.number || !(p.rect.overlaps q.rect)))

def solutionValid (b : Board) : Bool :=
  b.pieces.all (fun p =>
    p.rect.within b && shapeMatches p.shape p.rect &&
      (p.targetSize == none || p.targetSize == some p.rect.size)) &&
  b.pieces.all (fun p =>
    b.pieces.all (fun q => p.number == q.number || !(p.rect.overlaps q.rect))) &&
  b.cells.all (fun (x, y) => b.pieces.any (fun p => p.rect.contains x y))

end Patches