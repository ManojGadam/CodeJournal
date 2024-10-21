import { Button } from "react-bootstrap"
import "../styles/DrawerComponent.css" 
export function DrawerComponent(props){
    let dir = {}
    switch(props.direction){
        case "left":
            dir = {left:0,top:"10vh"}
            break
        case "right":
             dir = {right : 0,top:"10vh"}
             break
        case "top":
             dir = {top: "10vh",left:"50%",right:"50%",transform:"translate(-50%, 0%)"}
             break
        default:
            dir = {bottom : "10vh"}             
    }
    let width = 0
    switch(props.size){
        case "sm":
            width = {width:"40vw"} 
            break
        case "md": 
            width = {width:"60vw"} 
            break 
        case "lg": 
            width = {width:"75vw"} 
            break   
        default:
            width = {width:"100vw"}      
    }
    return(<div className="drawer" style={{...dir,...width}}>
             <div className="drawheader">
                <span style={{marginLeft:"4rem"}}>{props.header}</span>
                <div className="buttonContainer">
                <Button onClick={()=>props.handleClose()}>Close</Button>
                {props.readOnly?<></>:<Button onClick={()=>props.handleSubmit()} variant="primary">Submit</Button>}
                </div>
             </div>
             {props.renderComp(props)}   
        </div>)
}