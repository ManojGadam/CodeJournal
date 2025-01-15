import { AddProblemCard } from "../components/AddProblemCard"
import { ViewProblems } from "../components/ViewProblems"
import "../styles/problemContainer.css"
import { useState,useEffect } from "react"
import { getProblems,addProblem,updateProblem,pushToGit,SaveConfiguration } from "../apis/ProblemApis"
export function ProblemContainer(){
    //const [showCard,setShowCard] = useState(false)
    const [problems,setProblems] = useState([])
    const [editComments,setEditComments] = useState(false)
    const [problemKey,setProblemKey] = useState("")
    const [isLoading,setIsLoading] = useState(true)
    const [problem,setProblem] = useState({})
    const [isSubmitting,setIsSubmitting] = useState(false)
    const [configData,setConfigData] = useState({})

    useEffect(()=>{
        if(!editComments)return
        updateProblem(problem).then(()=>{setEditComments(prev=>!prev)})
    },[editComments])

    useEffect(()=>{
        if(!problemKey)return
        setIsSubmitting(true)
        addProblem(problemKey).then(()=>{
            GetProblems()
            setIsSubmitting(false)
            setProblemKey("")
        }).catch((er)=>{
            throw new Error(er)
        })
    },[problemKey])

    useEffect(()=>{
        GetProblems()
    },[editComments])

    function GetProblems(){
        getProblems().then((val)=>{
            setIsLoading(()=>false)
            setProblems(()=>val.data)
        }).catch((error)=>
            console.log(error)
        )
    }
    function handleGitClick(data){
        if(isSubmitting)return
        setIsSubmitting(true)
        pushToGit(data).then((res)=>{
            setIsSubmitting(false)
            console.log(res)
        })
    }
    const updateComment=(id,comment)=>{
        setProblem({id:id,comments:comment})
        setEditComments(prev=>!prev)
    }

    // const handleButtonOpen = () =>{
    //     setShowCard((prev)=>!prev)
    // }
    // function handleClose(){
    //     setShowCard((prev)=>!prev)
    // }

    // function setUrl(url){
    //     setProblemKey(url)
    // }
    const saveConfiguration=()=>{
        SaveConfiguration(configData).then((res)=>{
            console.log(res)
        })
    }
    return(
        <>  
        <ViewProblems
             problems = {problems}
             setEditComments = {setEditComments}
             isLoading = {isLoading}
             setConfigData = {setConfigData}
             updateComment = {updateComment}
             handleGitClick={handleGitClick}
             saveConfiguration = {saveConfiguration}
             isSubmitting={isSubmitting} //Might cause an issue using same variable for two things
              /> 
        </>      
    )
}