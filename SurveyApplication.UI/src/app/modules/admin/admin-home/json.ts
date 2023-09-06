export const json = {
  elements: [
    {
      type: "radiogroup",
      name: "product_discovering",
      title: "How did you first discover our product?",
      choices: [
        "Search engine",
        "GitHub",
        "Friend or colleague",
        {
          value: "Redit",
          text: "Reddit",
        },
        "Medium",
        "Twitter",
        "Facebook",
      ],
    },
    {
      type: "radiogroup",
      name: "useproduct",
      title: "Do you currently use our libraries?",
      isRequired: true,
      choices: ["Yes", "No"],
    },
    {
      type: "checkbox",
      name: "uselibraries",
      title: "Which libraries do you use?",
      choices: [
        {
          text: "Form Library",
          value: "Survey Library (Runner)",
        },
        {
          text: "Survey Creator",
          value: "Survey Creator (Designer)",
        },
      ],
    },
    {
      type: "rating",
      name: "nps_score",
      title:
        "How likely are you to recommend our product to a friend or colleague?",
    },
    {
      type: "radiogroup",
      name: "product_recommend",
      title: "Have you recommended our product to anyone?",
      choices: ["Yes", "No"],
    },
    {
      type: "checkbox",
      name: "javascript_frameworks",
      title: "Which JavaScript frameworks do you use?",
      showOtherItem: true,
      choices: [
        "React",
        "Angular",
        "jQuery",
        "Vue",
        "Meteor",
        "Ember",
        "Backbone",
        "Knockout",
        "Aurelia",
        "Polymer",
        "Mithril",
      ],
    },
    {
      type: "checkbox",
      name: "backend_language",
      title: "Which web backend programming languages do you use?",
      showOtherItem: true,
      choices: [
        "Java",
        "Python",
        "Node.js",
        "Go",
        "Django",
        {
          value: "Asp.net",
          text: "ASP.NET",
        },
        "Ruby",
      ],
    },
    {
      type: "checkbox",
      name: "supported_devices",
      title: "Which device types do you need to support?",
      isRequired: true,
      choices: [
        "Desktop",
        {
          value: "Tablete",
          text: "Tablet",
        },
        "Mobile",
      ],
    },
  ],
};
