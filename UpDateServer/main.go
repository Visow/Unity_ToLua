package main

import (
	"fmt"
	"log"
	"net/http"
)

func getVersion(w http.ResponseWriter, r *http.Request) {
	r.ParseForm()
	fmt.Fprintf(w, "{appversion = 0}")
}

func hello(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "the game of xxx updateServer!")
}

func main() {
	fsh := http.FileServer(http.Dir("F:/Unity/Project_1/UpDateServer/res"))
	http.Handle("/res/", http.StripPrefix("/res/", fsh))
	http.HandleFunc("/getVersion", getVersion)
	err := http.ListenAndServe(":7070", nil)
	if err != nil {
		log.Fatal("ListenAndServe :", err)
	}
}
