import{_ as p,A as s,k as _,l}from"./index.2ed6b847.js";import{C as d,c as a,a as m,o as t,b as o,d as i,F as u,k as v,B as f}from"./vendor.6aecaf39.js";const k={name:"Home",setup(){return d(async()=>{try{await _.GetKeeps()}catch(r){l.error(r)}}),{keeps:a(()=>s.keeps),myVaults:a(()=>s.MyVaults),vKeeps:a(()=>s.MyVaultKeeps)}}},y={class:"container-fluid"},K={class:"masonry-container"};function g(r,h,x,e,B,V){const c=m("Keep");return t(),o("div",y,[i("div",K,[(t(!0),o(u,null,v(e.keeps,n=>(t(),f(c,{keep:n,vaults:e.myVaults,vKeeps:e.vKeeps,class:"masonry-item"},null,8,["keep","vaults","vKeeps"]))),256))])])}var j=p(k,[["render",g],["__scopeId","data-v-55e1ce91"]]);export{j as default};
