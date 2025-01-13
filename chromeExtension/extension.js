chrome.tabs.onUpdated.addListener(async (tabId,changeInfo)=>{
    if(changeInfo.status == "complete"){
        chrome.scripting.executeScript({
            target : {tabId:tabId},
            func:async () =>{
                const PRODUCTION = "http://localhost:5000"
                const DEVELOPMENT = "https://localhost:7275"
                const xPathMap={
                    nameAndsno :" div > div.flex.w-full.flex-1.flex-col.gap-4.overflow-y-auto.px-4.py-5 > div.flex.items-start.justify-between.gap-4 > div.flex.items-start.gap-2 > div > a",
                    difficulty :" div > div.flex.w-full.flex-1.flex-col.gap-4.overflow-y-auto.px-4.py-5 > div.flex.gap-1 > div.relative.inline-flex.items-center.justify-center.text-caption.px-2.py-1.gap-1.rounded-full",
                    tags:" div > div.flex.w-full.flex-1.flex-col.gap-4.overflow-y-auto.px-4.py-5 > div.mt-6.flex.flex-col.gap-3 > div:nth-child(6) > div > div.overflow-hidden.transition-all > div",
                    //comments:" div > div.CodeMirror.cm-s-easymde.CodeMirror-wrap > div.CodeMirror-scroll > div.CodeMirror-sizer",
                    code:"div.flex.flex-1.flex-col.overflow-hidden.pb-2 > div.flex-1.overflow-hidden > div > div > div.overflow-guard > div.monaco-scrollable-element.editor-scrollable.vs-dark > div.lines-content.monaco-editor-background > div.view-lines.monaco-mouse-cursor-text",
                    accepted:" div > div > div > div.w-full.flex-1.overflow-y-auto > div > div.flex.w-full.items-center.justify-between.gap-4 > div.flex.flex-1.flex-col.items-start.gap-1.overflow-hidden > div.text-green-s.dark\\:text-dark-green-s.flex.flex-1.items-center.gap-2.text-\\[16px\\].font-medium.leading-6 > span",
                    parent:'div.flex.justify-between.py-1.pl-3.pr-1 > div.relative.flex.overflow-hidden.rounded.bg-fill-tertiary.dark\\:bg-fill-tertiary.\\!bg-transparent > div.flex-none.flex > div:nth-child(2)'
                }
                const submission = window.location.href.includes('submissions')
                const accepted = document.querySelector(xPathMap.accepted)
                let parent = document.querySelector(xPathMap.parent)
                const [probNo,probName]=document.querySelector(xPathMap.nameAndsno).innerText.split(".")

                const url = document.querySelector(xPathMap.nameAndsno).href
                const difficulty = document.querySelector(xPathMap.difficulty).innerText
                const tags = document.querySelector(xPathMap.tags).innerText.replaceAll("\n",",")
                // const probComments = document.querySelector(xPathMap.comments).innerText
                // console.log("probComments")
                let buttonDiv = document.createElement('div')
                buttonDiv.id = 'pushButton'
                buttonDiv.classList.add('pushButton')
                let button = document.createElement('button')
                button.textContent = 'Add'
                button.addEventListener('click',handleClick)
                buttonDiv.appendChild(button)
                function handleClick(){
                    
                    const code = document.querySelector(xPathMap.code).innerText
                    const domain = PRODUCTION
                    fetch(domain+"/api/Problem/AddProblemDetails",{
                        method:"POST",
                        headers: {
                            "Content-Type": "application/json",
                            "Authorization": "Bearer your-token"
                        },
                        body: JSON.stringify({
                            ProblemNumber:probNo,
                            name:probName,
                            uRL:url,
                            difficulty:difficulty,
                            tags:tags,
                            comments:null,
                            code:code,
                            id:probName+".leetcode"
                        })
                    }).then(response => response.json())
                    .then(data => {
                        console.log("Response from backend:", data);
                    })
                    .catch(error => {
                        console.error("Error connecting to backend:", error);
                    });
                }
                if(submission && accepted){
                    if (!document.getElementById('pushButton')) {
                        parent.appendChild(buttonDiv);
                    }
                }
            },
        })
    }
})
