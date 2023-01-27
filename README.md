
# Instructions: 
This is a template repository for creating a nuget package hosted in github, but to be build in Azure Devops and published to Azure Devops Artifacts. 
It produces a pre-release version with from the merge build when a pull request is active, and a release version when the pull request is merged. 
It also provides automatic versioning (using GitVersion) and produces Source Link enabled debug symbols, allowing debugging into the package. 

## One Time Setup
- This repository makes use of the shared pipeline elements in https://github.com/ArnoPeters/Pipelines 
  Make sure to follow its [readme.md](https://github.com/ArnoPeters/Pipelines/blob/main/README.md) and create a copy in your own github account before proceeding. 
- Access to a team project in Azure Devops environment:  (TODO: figure out exactly which rights / roles)
	- A login with authorization to create and run pipelines and service connections in that team project
    - publish to a An artifacts feed in that team project 

### Create a copy of this repo in your own Github account
- _Reason required Is it even possible to depend directly on this repo, even if public? If you are not a member, can you authenticate? --> TEST THIS and explain correctly_
- If you want to be able to pull future commits from this repo, or contribute with pull requests: [create a fork](https://docs.github.com/en/get-started/quickstart/fork-a-repo)
- If you just want to create a starting point: [use this repository as template](https://docs.github.com/en/github/creating-cloning-and-archiving-repositories/creating-a-repository-on-github/creating-a-repository-from-a-template).
- Set the new repository as [template repository](https://docs.github.com/en/github/creating-cloning-and-archiving-repositories/creating-a-repository-on-github/creating-a-template-repository).
- Delete this "One Time Setup" chapter from the readme.md in the new repository. 

## Starting a new nuget package
- Starting from this repository in your Github account, create a new repository for the code of your package using [this repository as template](https://docs.github.com/en/github/creating-cloning-and-archiving-repositories/creating-a-repository-on-github/creating-a-repository-from-a-template). 
- When use a public repository or paid Github account, it is recommended to [add the following branch protection rules](https://docs.github.com/en/github/administering-a-repository/defining-the-mergeability-of-pull-requests/managing-a-branch-protection-rule) to your root branch (usually 'main' or 'master')
  - Require pull request reviews before merging.
  - Dismiss stale pull request approvals when new commits are pushed.
  This will ensure that all code changes will have to produce a pre-release version of the package, and that any open pull requests will have to reset and produce new pre-release versions with the merged code, ensuring that all merged code can be properly tested before closing a pull request. 

//WIP below
### Adding the pipeline in Azure Devops. 
- Setup in AzDO (link to M$ instructions)

- Github may ask you to authenticate
- A service connection will automatically be added

- Check service connection before pressing SAVE or RUN (in azure project settings -> service connections) -> TODO: will this be a new connection when using the pipeline in a different team project within the same AzDO subscription?
- Adjust the following settings in azure-pipelines.yml
	- Name of the pipeline template repo in github
	- name of the service connection in the azure-pipelines.yml
	- Name of your main branch 
	- Target Artifacts feed in Azure Devops
  	    (TODO: make these vars very clear in the template)
		(TODO: set pipeline to default windows agent in cloud
    - Optionally: Set the product name in azurepipelines.yml. By default it shows the REPO name
    - 
### Visual Studio: adding the actual code
- Do not forget to add your Github account to your Visual Studio accounts, or 

### Adding Actual Code
- Checkout your new repository
- Create and switch to a branch - branches will turn into pre-release versions
  
- Add a class library that builds using SDK style projects (i.e. not a classic .Net Framework project)
  	- Go to the project's properties
		- Go to the package tab and enable "Publish nuget package". 
          	- Add licensing and/or an icon if you want
       		- Versioning will be handled by the pipeline - the values can be left on default. 
	- Add your code
	- Optionally
		- Adjust the first version to publish in GitVersion.yml (this is very useful if this pipeline is to replace one for an already existing package)
        - delete this instruction from the readme.md, and keep the part below the horizontal line to describe your nuget package
	- Commit and push. This can be repeated as often as you want: no builds will run until a pull request is created. 
		- Optional once the first RELEASE package has been deployed
        	- Force version increases using +semver: (instructions)
	 
### Releasing packages: pull requests in Github
- Create a pull request. This should trigger your pipeline to start running and produce a pre-release version. 
- Test your pre-release version. Make extra commits in case adjustments need to be made. Since a pull request is open every push generates a new pre-release version. 
- Once the most recent pre-release version is accepted: close the pull request. This should trigger your pipeline to start running and produce a final release version. 
	- If you have branch policies in place, this also resets other open pull requests, making sure you have no version collisions when a publish attempt is made. 
     
---

(Perhaps keep this in Blog? )

- Configure Visual Studio for Symbol Link (debugging into the nuget package)
  - Add symbol server
  - Enable Source Link
  - Disable "Just my Code"
- To test Symbol Link
    - Consume your package in project you can start
    - Set a breakpoint right before a call into the package
    - Use F11 to Step Into the package
    - Symbol Link should pop up a dialog, allowing you to download the source / allow always downloading
    - After downloading the source, you should see the exact code used to build the package waiting on a breakpoint inside. 
    - 
- Troubleshooting
	-  View Modules
	-  Load symbols

---

1. **About the application.**

    _TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project._

1. **Getting Started**  
   TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
      1.	Installation process
      2.	Software dependencies
      3.	Latest releases
      4.	API references

1. **Build and Test**  
   _TODO: Describe and show how to build your code and run the tests._

1. **Contribute**  
   _TODO: Explain how other users and developers can contribute to this repo._
