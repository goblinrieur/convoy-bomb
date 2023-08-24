#! /usr/bin/env bash

filename=SGF.fs
runner=$(which gforth-fast)

if [[ ${runner} != *"gforth-fast"* ]] ; then 
	 echo "Gforth interpreter not found"
	 exit 1
fi
trap '' SIGINT
gnome-terminal --geometry=53x10+x+y --zoom=2 --command="$runner -W $filename" 2> /dev/null
clear 
trap SIGINT
tput cnorm
exit $?
