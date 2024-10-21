import { useState } from "react";
import { Button, Form } from "react-bootstrap";
import "../styles/problemContainer.css"
export function AddProblemCard(args){
    const [url,setUrl] = useState("")
    const [error,setError] = useState("")
    const handleSubmit = (e) =>{
        e.preventDefault()
        if(args.isSubmitting)return
        if(url.slice(0,30)!=="https://leetcode.com/problems/"){
            setError("Enter an url for a leetcode problem")
            return
        }
        args.setUrl(url)
        args.handleClose((prev)=>!prev)
    }
    return(
        <div className="addProblemCard">
        <Form onSubmit={handleSubmit} className="formProblemCard">
            <label htmlFor={url}>Enter the url for the problem</label>
            <textarea 
                name={url}
                id={url}
                onChange={e=>setUrl(e.target.value)}
            />
            <p className="error">{error}</p>
            <input type="button" onClick={()=>args.handleClose((prev=>!prev))} value="close" />
            <Button type="submit" size="xs" disabled={args.isSubmitting}>submit</Button>
        </Form>
        </div> 
    )
}