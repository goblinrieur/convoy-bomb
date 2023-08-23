\ https://www.vertex42.com/ExcelTips/unicode-symbols.html
\ adapt here your prefered unicode char each 
128741 constant boat		\ unicode boat 
128713 constant man			\ unicode guy 
128641 constant helicopter	\ unicode helicopter 
40 constant path			\ determinates path
16 constant boat_x			\ boat default X coordonate
0 constant heli_y			\ helicopter can be on first line
\
: pose_bat	( y x -- uc ) at-xy boat xemit ;
: pose_hom	( y x -- uc ) at-xy man xemit ;
: pose_hel	( y x -- uc ) at-xy 128641 xemit ;
: pose_vid	( y x -- uc ) at-xy bl emit ;
\
: shortdelay 80 ms ;	\ seems good to both be fast & limit blink effect
\
: testpoc ( -- )
	.\" \e[?25l" page					\ clear screen & hide cursor 
	boat_x 5 pose_bat shortdelay		\ draws fixed coordonates boat
	path 0 do
	    path i - dup					\ comes from right to left
		heli_y pose_hel shortdelay
	    boat_x = if  
			5 1 do
				boat_x i pose_hom shortdelay	\ drop a man 
				boat_x i pose_vid shortdelay 	\ clean this coordonate
			loop
         	THEN 
	loop								\ continue flight
	7 0 do 
		cr								\ to keep drawings before exit
	loop
.\" \e[?25h" cr 0 (bye)					\ restore cursor & exit
;
\
testpoc
