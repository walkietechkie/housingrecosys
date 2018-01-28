;;;======================================================
;;;     Housing Recommendation Expert System
;;;
;;;     This engine will help prospective house buyers
;;;	in Singapore to choose their future home.
;;;
;;;     CLIPS Version 6.24
;;;
;;;     KE4102 CA
;;;======================================================


;;;*****************
;;;* Definition Mapping File *
;;;*****************
(deffacts text-for-id
(text-for-id 
   (id no)
   (text "no"))
(text-for-id 
   (id yes)
   (text "yes"))

(text-for-id 
   (id HasBudgetLevel0)
   (text "Do you have budget?"))

(text-for-id 
   (id HasBudgetLevelY)
   (text "Condo for you!"))

(text-for-id 
   (id HasBudgetLevelN)
   (text "HDB for you!"))
)