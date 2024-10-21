import { useState,useRef } from "react"
import { Button, Placeholder } from "react-bootstrap"
import {Link} from "react-router-dom"
import { MdEdit } from "react-icons/md";
import { IoLogoGithub } from "react-icons/io";
import { DrawerComponent } from "./DrawerComponent";
import { FaEye } from "react-icons/fa";
import { Textarea } from "@chakra-ui/react"

export function ViewProblems(args){
    const [openDrawer,setOpenDrawer] = useState(false)
    const [isreadOnly,setIsreadOnly] = useState(true)
    const [clickedProblem,setClickedProblem] = useState({comments:"",id:-1,ind:-1})
    const comment = useRef({})
    const [showTags,setShowTags] = useState(false)
    const [showConfig,setShowConfig] = useState(false)

    const handleEditClick=(index)=>{
        handleClose(index)
        setIsreadOnly(false)
    }

const submitComment = (ind,id) =>{
    args.updateComment(id,comment.current.value)
    handleClose(ind)
}
function handleClose(ind){
    setClickedProblem({comments:args.problems[ind].comments,ind:ind,id:args.problems[ind].id})
    setOpenDrawer((prev)=>!prev)
    setIsreadOnly(true)
}

const commentsArea = (props)=>{
   return <Textarea disabled={props.readOnly} className="txtAreaDrawer" ref={(el) => props.comment.current = el} defaultValue={props.data} />
}

const handleConfigClose=()=>{
    args.setConfigData({})
    setShowConfig((prev)=>!prev)
}

const handleConfigSubmit=()=>{
    args.saveConfiguration()
    handleConfigClose()
}
const configArea = () =>{
    const handleChange = (e) => {
        const { name, value } = e.target;
        args.setConfigData((prevData) => ({
          ...prevData,
          [name]: value,
        }));
      };
    return <form className="configForm">
        <label htmlFor="gitToken" className="posRel">Enter your Github Token   <Link className="helpLink" target="blank" to="https://scribehow.com/shared/Generating_a_personal_access_token_on_GitHub__PUPxxuxIRQmlg1MUE-2zig">Need help?</Link></label>
        <input type="text" name="gitToken" onChange={handleChange} className="configInp" />
        <label htmlFor="leetToken" className="posRel">Enter your LeetCode Token <Link className="helpLink" target="blank" to="https://scribehow.com/shared/Generating_a_personal_access_token_on_GitHub__PUPxxuxIRQmlg1MUE-2zig">Need help?</Link></label>
        <input type="text" name="leetToken" onChange={handleChange} className="configInp" />
        <label htmlFor="gitURL">Enter your Github Repository URL</label>
        <input type="text" name="gitURL" onChange={handleChange} className="configInp" />
    </form>
}

    return (
        <div className="problemContainer">
        <div className="pblmButtonContainer">   
        <Button onClick={()=>setShowConfig((prev)=>!prev)}>Edit Configuration</Button>    
        <Button onClick={args.handleButtonOpen}>+</Button>
        </div>
        {showConfig?
        <DrawerComponent
            direction = {"top"}
            size = {"md"}
            header = "Edit Configuration"
            handleSubmit = {handleConfigSubmit}
            handleClose = {handleConfigClose}
            renderComp = {configArea}
         />
        :<></>}
        <div>
        <div className="checkContainer">   
        <span>List of solved problems</span>
        <span><input type="checkbox" defaultChecked={showTags} onChange={()=>setShowTags((prev)=>!prev)} />Show Tags</span>
        </div>
         {
            args.isLoading?
            <p><Placeholder aria-hidden="true" /></p>
             :args.problems.map((problem,ind)=>{
               let cName = "name" +" " +problem.difficulty
               let tags = problem.tags.split(",")
               return(
                <>
               <div className="pblm" key={ind}>
                    <Link to={problem.link} className="noColor" target="_blank">
                    <span className={cName}>{problem.name}</span>
                    </Link>
                    <IoLogoGithub className={args.isSubmitting ? "disabled" : ""} aria-disabled={args.isSubmitting} onClick={()=>{args.handleGitClick({id:problem.id,questionSlug:problem.titleSlug,questionNumber:problem.problemNumber})}}/>
                    {openDrawer?(<DrawerComponent
                        direction = {"right"}
                        size = {"md"}
                        header = "Comments"
                        comment = {comment}
                        data = {clickedProblem.comments}
                        handleSubmit = {()=>submitComment(clickedProblem.ind,clickedProblem.id)}
                        handleClose = {()=>handleClose(ind)}
                        readOnly = {isreadOnly}
                        renderComp = {commentsArea}
                     />):<></>}    
                    {!problem.comments?
                        <Button className="commentsAdd" onClick={()=>handleEditClick(ind)}>+</Button>
                     :
                       ( <div className="viewEditContainer">
                         <FaEye onClick={()=>handleClose(ind)}/>   
                        <MdEdit onClick={()=>handleEditClick(ind)} />
                        </div>)
    
                }
                  <div>
                    {showTags?tags.map((tag,i)=>{
                       return <span key={i} className="tag">{tag}</span>
                    }):<></>}
                  </div> 
                </div>
                </>
                )
            })
         }
         </div>   
        </div>
    )
}