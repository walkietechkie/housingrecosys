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
;;;* Configuration *
;;;*****************


;;; ***************************
;;; * DEFTEMPLATES & DEFFACTS *
;;; ***************************
(defrule starter
;; no condition for this rule - hence always fire
=>	(printout t "Do you have budget?" crlf)
	(bind ?answer (read))
	(assert (has-budget ?answer)))

;; simple check for now
(defrule condo-able
	(has-budget yes) 
=>	(printout t "Condo for you!" crlf))

;;; Go get some petrol
(defrule buy-hdb
	(has-budget no) 
=>	(printout t "HDB for you!" crlf))