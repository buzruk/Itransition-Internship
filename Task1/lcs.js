const args=process.argv.slice(2);let res="";if(1===args.length)res=args[0];else if(args.length>1){const e=args.reduce((e,s)=>s.length<e.length?s:e);for(let s=0;s<e.length;s++)for(let l=s+1;l<=e.length;l++){const r=e.slice(s,l);args.every(e=>e.includes(r))&&r.length>res.length&&(res=r)}}console.log(res);