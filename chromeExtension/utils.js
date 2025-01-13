const xPathMap={
    nameAndsno : document.querySelector(" div > div.flex.w-full.flex-1.flex-col.gap-4.overflow-y-auto.px-4.py-5 > div.flex.items-start.justify-between.gap-4 > div.flex.items-start.gap-2 > div > a"),
    difficulty : document.querySelector(" div > div.flex.w-full.flex-1.flex-col.gap-4.overflow-y-auto.px-4.py-5 > div.flex.gap-1 > div.relative.inline-flex.items-center.justify-center.text-caption.px-2.py-1.gap-1.rounded-full.bg-fill-secondary.text-difficulty-medium.dark\\:text-difficulty-medium"),
    tags: document.querySelector(" div > div.flex.w-full.flex-1.flex-col.gap-4.overflow-y-auto.px-4.py-5 > div.mt-6.flex.flex-col.gap-3 > div:nth-child(6) > div > div.overflow-hidden.transition-all > div"),
    comments: document.querySelector(" div > div.CodeMirror.cm-s-easymde.CodeMirror-wrap > div.CodeMirror-scroll > div.CodeMirror-sizer"),
    code: document.querySelector("div.flex.flex-1.flex-col.overflow-hidden.pb-2 > div.flex-1.overflow-hidden > div > div > div.overflow-guard > div.monaco-scrollable-element.editor-scrollable.vs-dark > div.lines-content.monaco-editor-background > div.view-lines.monaco-mouse-cursor-text")
}

const PRODUCTION = "http://localhost:5000"
const DEVELOPMENT = "https://localhost:7275"

//module.exports = {PRODUCTION,DEVELOPMENT}
//export {xPathMap};