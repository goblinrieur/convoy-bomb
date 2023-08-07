#! /usr/bin/gforth
( CONVOY BOMB ZX81 FÖR GFORTH & XTERM & UNICODE. IN SWEDISH)
( GRAFISKA KOMMANDON: PLOT                           UNPLOT)
FORTH DEFINITIONS HEX ( Z80 MASKINKOD FÖR ZX81 )
\ : PIT ;CODE E1 C, D1 C, C5 C, 4D C, 43 C, CD C, 0BB2 , C1 C,
\   C3 C, NEXT , SMUDGE PIT
\ : PIT1 4030 C! PIT ;
\ : PLOT1 ( Y X --- ) 9B PIT1 ;
\ : UNPLOT1  ( Y X --- ) A0 PIT1 ;
DECIMAL
REQUIRE random.fs
UTIME DROP SEED !
( INKLUDERA PLOT OCH UNPLOT)
INCLUDE ../semigraphics/semigraphics.fs
: HOME 0 0 AT-XY ;
: CLS PAGE ;
: PLOT1 SWAP PLOT ;
: UNPLOT1 SWAP UNPLOT ;
24 _ROWS !
: DR0J 10 MS ;
: ?INNANF0R OVER OVER >R >R R@ -1 > R@ 44 < AND R> DROP R@ -1 >
  R@ 64 < AND R> DROP AND ;
\ : 2DROP DROP DROP ;
: PLOT ?INNANF0R IF PLOT1 ELSE 2DROP ENDIF DR0J ;
: UNPLOT ?INNANF0R IF UNPLOT1 ELSE 2DROP ENDIF DR0J ;
\ ;S

( CONVOY BOMB SCR 1     1983-05-07)
FORTH DEFINITIONS DECIMAL
( SCR # 21 <GRAFIK> LADDAD.)
: TASK ;
VARIABLE VB
VARIABLE N  VARIABLE AB
: VISA-P HOME ." CONVOY BOMB" 10 SPACES ." POÄNG:" N @ 5 .R ;
VARIABLE XF  VARIABLE YF
: FLYGPLAN YF @ 1+ XF @ PLOT YF @ XF @ 6 + PLOT ;

: FLYG YF @ 1+ XF @ 1 - UNPLOT YF @ XF @ 1 - UNPLOT FLYGPLAN
  1 XF +! XF @ 65 = IF
  -7 XF ! 256 RANDOM ( 16436 C@) 9 / 12 + YF ! VB @ IF
  -117 N +! N @ 0< IF 0 N ! THEN VISA-P THEN THEN ;
\ -->

( CONVOY BOMB SCR 2 SAKNAS P.G.A. ETT MISSTAG NÄR JAG KOPIERADE
  TILL RAMDISK 1983. ALLA BACKUPER SOM HITTATS ÄR FEL.
  REKONSTRUERAD & OPTIMERAD 2023-08-07.)
VARIABLE XS  VARIABLE YS
: SKEPP YS @ 5 + XS @ 11 + PLOT
        YS @ 4 + XS @ 9 + PLOT
        YS @ 3 + XS @ 9 + PLOT
\ -->

( CONVOY BOMB SCR 3     1983-05-07)
( SKEPP DEF. FORTS. >> )
        YS @ 2 + XS @ PLOT
        YS @ 1+ XS @ 2 + PLOT
        YS @ XS @ 3 + PLOT ;

: FLYT -1 XS +! SKEPP YS @ 5 + XS @ 12 + UNPLOT
  YS @ 4 + XS @ 13 + UNPLOT YS @ 3 + XS @ 13 + UNPLOT
  YS @ 2 + XS @ 15 + UNPLOT YS @ 1+ XS @ 14 + UNPLOT
  YS @ XS @ 13 + UNPLOT XS @ -14 < IF
  1 AB +! 64 XS ! THEN ;
: VATTEN HOME 21 0 DO CR LOOP 32 0 DO 9618 XEMIT LOOP ;
: VISA 0 VB ! VATTEN -6 XF ! 2 YS ! 64 XS ! BEGIN 40 YF ! FLYG
  FLYT FLYG KEY? IF
  KEY 13 = ELSE 0 THEN ( 16421 @ -577 =) UNTIL ;
\ -->

( CONVOY BOMB SCR 4     1983-05-07)
VARIABLE XB VARIABLE RB 4 RB ! VARIABLE ?BOMB VARIABLE ?S -1 ?S !
: BOM 2 XB @ 1 - PLOT 2 XB @ 1+ PLOT 3 XB @ PLOT 3 XB @ 2 +
  PLOT 4 XB @ 1+ PLOT ;
: SUDDA HOME 17 0 DO CR LOOP 4 0 DO 32 SPACES CR LOOP ;
: EXPLODERA SUDDA 13 5 12 4 13 4 10 3 9 5 2 4 4 6 6 6 6 5 0 3
  0 1 15 3 14 0 8 3 6 3 4 3 3 2 3 1 2 1 3 0 13 1 12 2 12 1
  11 1 11 0 4 1 5 2 6 2 7 2 8 1 6 1 5 0 9 0 8 0 6 0 7 0 36 0
  DO YS @ + SWAP XS @ + PLOT LOOP 1 AB +! ;
VARIABLE YB
: ?TRAFF YB @ RB @ - 3 < IF XB @ XS @ 3 + > XB @ XS @ 14 + <
  AND IF EXPLODERA 375 N +! VISA-P 72 XS ! ELSE BOM THEN VATTEN 
  0 ?BOMB ! 6 ?S ! BEGIN KEY? WHILE KEY DROP REPEAT THEN ;
: FALL YB @ RB @ - XB @ UNPLOT YB @ RB @
  13 * 10 / DUP RB ! - XB @ 1+ DUP XB ! PLOT ?TRAFF ;
: BOMBINIT KEY? IF
  KEY DUP [CHAR] b = SWAP [CHAR] B = OR ELSE 0 THEN
  ( 16421 @ -8321 =) IF
  1 ?BOMB ! XF @ 4 + XB ! 4 RB ! YF @ 3 + YB ! THEN ;
\ -->

( CONVOY BOMB SCR 5     1983-05-07)
: B 0 N ! CLS VISA-P 0 AB ! 1 VB !  VATTEN
  BEGIN FLYG FLYT ?BOMB @ IF
  FALL ELSE BOMBINIT THEN -1 ?S +! FLYG ?S @ 0= IF
  SUDDA THEN AB @ 5 = UNTIL ;
: INST CLS CR CR 6 SPACES ." C O N V O Y   B O M B" CR CR
  ." DITT UPPDRAG ÄR ATT SÄNKA ALLA" CR
  ." FARTYG I EN KONVOJ. KONVOJEN" CR
  ." BESTÅR AV 5 HANDELSFARTYG." CR
  ." DU FÅR POÄNG FÖR VARJE SÄNKT" CR
  ." SKEPP OCH POÄNGAVDRAG FÖR VARJE" CR
  ." ÖVERFLYGNING." CR
  ." FÖR ATT FÄLLA EN BOMB, TRYCK NER" CR
  ." <B> TILLS DU SER BOMBEN FALLA." CR
  ." DU KAN INTE FÄLLA NÄSTA BOMB" CR
  ." FÖRRÄN DEN FÖRRA HAR LANDAT." CR
  ." DU KAN EJ REGLERA HÖJDEN EFTER-" CR
  ." SOM DU EJ ÄR PILOT UTAN BOMB-" CR
  ." FÄLLARE." CR
  ." FÖR ATT BÖRJA, TRYCK PÅ <N/L>." ;
: CONVOY-BOMB HIDE-CUR BEGIN INST VISA B HOME CR CR CR CR
  ." VILL DU SPELA EN GÅNG TILL?(J/N)"
  0 BEGIN DROP KEY DUP DUP [CHAR] n = SWAP [CHAR] j = OR UNTIL
  [CHAR] n = UNTIL CLS
  ." ADJÖ. HOPPAS NI HADE TREVLIGT." CR SHOW-CUR ;
\ ;S
